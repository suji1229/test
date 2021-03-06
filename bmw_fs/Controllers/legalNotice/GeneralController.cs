﻿using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.legalNotice;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.legalNotice;
using Ganss.XSS;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.legalNotice
{
    [Authorize(Roles = "MASTER, COMPLIANCE")]
    public class GeneralController : Controller
    {
        GeneralService generalService = new GeneralServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        public ActionResult list(General general)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(general, 20, generalService.findAllCount(general));
            ViewBag.list = generalService.findAll(general);
            ViewBag.pagination = general;

            return View("~/Views/LegalNotice/General/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/LegalNotice/General/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(General general)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            var sanitizer = new HtmlSanitizer();
            general.contents = sanitizer.Sanitize(general.contents);
            general.regId = System.Web.HttpContext.Current.User.Identity.Name;
            generalService.insertGeneral(multipartfiles, general);
            return RedirectToAction("list");
        }

        public ActionResult view(General general)
        {
            General item = generalService.findGeneral(general);
            ViewBag.item = item;
            ViewBag.files = filesService.findAllByMasterIdxAndType(item.idx, "file");
            return View("~/Views/LegalNotice/General/view.cshtml");
        }

        public ActionResult modify(General general)
        {
            General item = generalService.findGeneral(general);
            ViewBag.item = item;
            ViewBag.files = filesService.findAllByMasterIdxAndType(item.idx, "file");
            return View("~/Views/LegalNotice/General/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(General general)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            var sanitizer = new HtmlSanitizer();
            general.contents = sanitizer.Sanitize(general.contents);
            general.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            generalService.updateGeneral(multipartRequest, general);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(General general)
        {
            generalService.deleteGeneral(general);
            return RedirectToAction("list");
        }

    }
}