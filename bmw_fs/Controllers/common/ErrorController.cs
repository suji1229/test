﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.common
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }
    }
}