using DataAccessLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult IsUserExist(string User)
        {
            using (MABRUKLISTEntities dbContext = new MABRUKLISTEntities())
            {
                return Json(!dbContext.AspNetUsers.Any(x => x.UserName == User), JsonRequestBehavior.AllowGet);
            };
        }
        public JsonResult IsEmailExist(string Email)
        {
            using (MABRUKLISTEntities dbContext = new MABRUKLISTEntities())
            {
                return Json(!dbContext.AspNetUsers.Any(x => x.Email == Email), JsonRequestBehavior.AllowGet);
            };
        }
    }
}