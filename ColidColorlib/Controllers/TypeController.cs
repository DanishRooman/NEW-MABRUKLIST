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

                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_type.Where(x => x.type_name == dto.Type).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Group already exist" }, JsonRequestBehavior.AllowGet);
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

                        }
                        else
                        {
                            var data = dbcontext.mblist_type.Find(dto.id);
                            if (data != null)
                            {
                                data.type_name = dto.Type;
                                dbcontext.SaveChanges();
                                return Json(new { key = false, value = "Type Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Type is not found" }, JsonRequestBehavior.AllowGet);
                            }
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
        [HttpGet]
        public ActionResult GetType(int id)
        {
            try
            {
                TypeDto Type = new TypeDto();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var type = dbcontext.mblist_type.Find(id);
                    if (type != null)
                    {
                        Type.id = type.type_key;
                        Type.Type = type.type_name;
                        return PartialView("AddType", Type);

                    }
                    else
                    {

                        return Json(new { key = false, value = "Type not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the type" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteType(int id)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var type = dbcontext.mblist_type.Find(id);
                    if (type != null)
                    {
                        dbcontext.mblist_type.Remove(type);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Type deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        return Json(new { key = false, value = "Type not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Type" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}