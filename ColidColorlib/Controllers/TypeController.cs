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
        [HttpGet]
        //Select
        public ActionResult TypeListing()
        {
            List<TypeDto> TypeList = new List<TypeDto>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                TypeList = dbcontext.mblist_type.AsEnumerable().OrderByDescending(x => x.type_key).Select(x => new TypeDto
                {
                    id = x.type_key,
                    Type = x.type_name
                }).ToList();
            };
            return PartialView("_TypeListing", TypeList);
        }

        [HttpPost]
        //insertion
        public ActionResult AddOrUpdateType(TypeDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        var data = dbcontext.mblist_type.Where(x => x.type_name == dto.Type).FirstOrDefault();
                        if (data != null)
                        {
                            return Json(new { key = false, value = "Category already exist" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            mblist_type type = new mblist_type()
                            {
                                type_name = dto.Type
                            };

                            dbcontext.mblist_type.Add(type);
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Type added successfully" }, JsonRequestBehavior.AllowGet);
                        }


                    };
                }
                else
                {
                    return Json(new { key = false, value = "Please enter correct data" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to save the Type" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}