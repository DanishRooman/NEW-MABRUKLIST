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

        [HttpPost]
        public ActionResult AddOrUpdateCategory(CategoryDto dto)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    mblist_category category = new mblist_category()
                    {
                        cat_name = dto.Category
                    };
                    dbcontext.mblist_category.Add(category);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "Category added successfully" }, JsonRequestBehavior.AllowGet);
                };
            }
            catch(Exception ex)
            {
                return Json(new { key = false, value = "Unable to save the category" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}