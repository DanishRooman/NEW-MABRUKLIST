using DataAccessLayer.DBContext;
using DataTransferObjects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class TypeController : Controller
    {
        // GET: Type
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddType()
        {
            TypeDto Type = new TypeDto();
            return PartialView("AddType", Type);
        }
        [HttpPost]
        public ActionResult AddOrUpdateType(TypeDto dto)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext=new MABRUKLISTEntities())
                {
                    mblist_type type = new mblist_type()
                    {
                        type_name=dto.Type
                    };

                    dbcontext.mblist_type.Add(type);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "Type added successfully" }, JsonRequestBehavior.AllowGet);
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to save the Type" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}