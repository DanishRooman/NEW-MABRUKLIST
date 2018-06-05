using DataAccessLayer.DBContext;
using DataTransferObjects.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            CategoryDto category = new CategoryDto();
            return PartialView("AddCategory", category);
        }

        [HttpGet]
        //select
        public ActionResult CategoryListing()
        {
            List<CategoryDto> categoryList = new List<CategoryDto>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                categoryList = dbcontext.mblist_category.AsEnumerable().OrderByDescending(x => x.cat_key).Select(x => new CategoryDto
                {
                    Id = x.cat_key,
                    Category = x.cat_name
                }).ToList();
            };
            return PartialView("_CategoryListing", categoryList);
        }

        [HttpPost]
        public ActionResult AddOrUpdateCategory(CategoryDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.Id == 0)
                        {
                            var data = dbcontext.mblist_category.Where(x => x.cat_name == dto.Category).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Category already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_category category = new mblist_category()
                                {
                                    cat_name = dto.Category
                                };
                                dbcontext.mblist_category.Add(category);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Category added successfully" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            var data = dbcontext.mblist_category.Find(dto.Id);
                            if (data != null)
                            {
                                data.cat_name = dto.Category;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Category updated successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Category not found" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                       

                    };
                }
                else
                {
                    return Json(new { key = false, value = "Please enter correct data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to save the category" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCategory(int id)
        {
            try
            {
                CategoryDto category = new CategoryDto();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var cat = dbcontext.mblist_category.Find(id);
                    if (cat != null)
                    {
                        category.Id = cat.cat_key;
                        category.Category = cat.cat_name;
                        return PartialView("AddCategory", category);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Category not found its deleted" }, JsonRequestBehavior.AllowGet);
                    }
                };
                
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to edit the category" }, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}