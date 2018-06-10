using DataAccessLayer.DBContext;
using DataTransferObjects.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCategories()
        {

            CategoriesDTO CAT = new CategoriesDTO();
            return PartialView("_AddCategories", CAT);
        }
        [HttpGet]
        public ActionResult CategoriesListing()
        {

            List<CategoriesDTO> CatList = new List<CategoriesDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                CatList = dbcontext.mblist_Category_Table.AsEnumerable().OrderByDescending(x => x.Category_key).Select(x => new CategoriesDTO
                {
                    id = x.Category_key,
                    Category = x.Category_Name

                }).ToList();
            };
            return PartialView("_CategoriesListing", CatList);
        }
        [HttpPost]
        public ActionResult AddOrUpdateCategoreis(CategoriesDTO dto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_Category_Table.Where(x => x.Category_Name == dto.Category).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Group already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_Category_Table CATtb = new mblist_Category_Table()
                                {
                                    Category_Name = dto.Category
                                };
                                dbcontext.mblist_Category_Table.Add(CATtb);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Category added successfully" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        {
                            var data = dbcontext.mblist_Category_Table.Find(dto.id);
                            if (data != null)
                            {
                                data.Category_Name = dto.Category;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Category Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Category is not found" }, JsonRequestBehavior.AllowGet);
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

                return Json(new { key = false, value = "Unable to save the Category" }, JsonRequestBehavior.AllowGet); ;
            }

        }

        public ActionResult GetCategories(int id)
        {

            try
            {
                CategoriesDTO Catgr = new CategoriesDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var cat = dbcontext.mblist_Category_Table.Find(id);
                    if (cat != null)
                    {
                        Catgr.id = cat.Category_key;
                        Catgr.Category = cat.Category_Name;
                        return PartialView("_AddCategories", Catgr);

                    }
                    else
                    {
                        return Json(new { key = false, value = "Category not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Category" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCategories(int id) {

            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var ctgry = dbcontext.mblist_Category_Table.Find(id);
                    if (ctgry != null)
                    {
                        dbcontext.mblist_Category_Table.Remove(ctgry);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Category deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Category not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Category" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}