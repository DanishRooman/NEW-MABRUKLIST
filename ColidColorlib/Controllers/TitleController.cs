using DataAccessLayer.DBContext;
using DataTransferObjects.Group;
using DataTransferObjects.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class TitleController : Controller
    {
        // GET: Title
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddTitle()

        {
            TitleDTO Td = new TitleDTO();
            return PartialView("_AddTitle", Td);
        }
        [HttpGet]

        public ActionResult TitleListing()
        {


            List<TitleDTO> TitleList = new List<TitleDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                TitleList = dbcontext.mblist_title.AsEnumerable().OrderByDescending(x => x.title_key).Select(x => new TitleDTO
                {
                    id = x.title_key,
                    Title = x.title_name

                }).ToList();
            };
            return PartialView("_TitleListing", TitleList);

        }

        [HttpPost]

        public ActionResult AddOrUpdateTitle(TitleDTO dto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_title.Where(x => x.title_name == dto.Title).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Title already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_title title = new mblist_title()
                                {
                                    title_name = dto.Title
                                };
                                dbcontext.mblist_title.Add(title);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Title added successfully" }, JsonRequestBehavior.AllowGet);

                            }

                        }
                        else
                        {

                            var data = dbcontext.mblist_title.Find(dto.id);
                            if (data != null)
                            {
                                data.title_name = dto.Title;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Title Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Title is not found" }, JsonRequestBehavior.AllowGet);
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

                return Json(new { key = false, value = "Unable to save the Title" }, JsonRequestBehavior.AllowGet); ;
            }



        }
        public ActionResult GetTitle(int id)
        {


            try
            {

                TitleDTO Title = new TitleDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var title = dbcontext.mblist_title.Find(id);
                    if (title != null)
                    {
                        Title.id = title.title_key;
                        Title.Title = title.title_name;
                        return PartialView("_AddTitle", Title);

                    }
                    else
                    {
                        return Json(new { key = false, value = "Title not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult DeleteTitle(int id)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var title = dbcontext.mblist_title.Find(id);
                    if (title != null)
                    {
                        dbcontext.mblist_title.Remove(title);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Title deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Title not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Title" }, JsonRequestBehavior.AllowGet);
            }

        }


    }
}