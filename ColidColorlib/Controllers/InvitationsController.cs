﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class InvitationsController : Controller
    {
        // GET: Invitations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUploadModal()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadInvitations()
        {
            int count = Request.Files.Count;
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}