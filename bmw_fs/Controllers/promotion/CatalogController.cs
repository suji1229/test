﻿using bmw_fs.Models.catalog;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.catalog;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.catalog;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.catalog
{
    [Authorize(Roles = "MASTER, MSS")]
    public class CatalogController : Controller
    {
        CatalogService catalogService = new CatalogServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        // GET: Catalog
        public ActionResult list(Catalog catalog)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(catalog, 20, catalogService.findAllCount(catalog));
            ViewBag.list = catalogService.findAll(catalog);
            ViewBag.pagination = catalog;
            return View("~/Views/Promotion/Catalog/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/Promotion/Catalog/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Catalog catalog)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            catalog.regId = System.Web.HttpContext.Current.User.Identity.Name;
            catalogService.insertCatalog(multipartfiles, catalog);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Catalog catalog)
        {
            Catalog item = catalogService.findCatalog(catalog);
            ViewBag.item = item;
            ViewBag.files = filesService.findAllByMasterIdxAndType(item.idx, "file");
            return View("~/Views/Promotion/Catalog/view.cshtml");
        }

        public ActionResult modify(Catalog catalog)
        {
            Catalog item = catalogService.findCatalog(catalog);
            ViewBag.item = item;
            ViewBag.files = filesService.findAllByMasterIdxAndType(item.idx, "file");
            return View("~/Views/Promotion/Catalog/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Catalog catalog)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            catalog.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            catalogService.updateCatalog(multipartRequest, catalog);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Catalog catalog)
        {
            catalogService.deleteCatalog(catalog);
            return RedirectToAction("list");
        }

        [HttpPost]
        public ActionResult isDuplicated(Catalog catalog)
        {
            int caseCode = 0;
            if (catalogService.findCatalogDuplicated(catalog))
            {
                caseCode = -1;  //중복
            }
            else
            {
                caseCode = 1;
            }

            return Json(caseCode, JsonRequestBehavior.DenyGet);
        }
    }
}