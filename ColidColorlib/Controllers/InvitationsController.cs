﻿using DataAccessLayer.DBContext;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (Request.Files.Count > 0)
            {
                try
                {
                    string dbfile = "";
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname = "";
                        dbfile = "";
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        var myUniqueFileName = Guid.NewGuid().ToString();
                        fname = myUniqueFileName + "-" + fname;
                        dbfile = fname;
                        if (!Directory.Exists(Server.MapPath("~/Invitations/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Invitations/"));
                        }
                        fname = Path.Combine(Server.MapPath("~/Invitations/"), fname);
                        file.SaveAs(fname);
                        using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                        {
                            mblist_invitation_cards card = new mblist_invitation_cards()
                            {
                                invitation_img_path = "/Invitations/" + dbfile
                            };
                            dbcontext.mblist_invitation_cards.Add(card);
                            dbcontext.SaveChanges();
                        };
                    }
                    return Json(new { key = true, value = "Invitation card saved successfully", path = dbfile }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { key = false, value = "Unable to process your request. Please contact your admin." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { key = false, value = "No file is selected." }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}