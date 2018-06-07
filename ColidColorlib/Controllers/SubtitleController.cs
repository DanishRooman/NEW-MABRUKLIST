using DataAccessLayer.DBContext;
using DataTransferObjects.Subtitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class SubtitleController : Controller
    {
        // GET: Subtitle
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddSubtitle()
        {
            SubtitleDTO subtile = new SubtitleDTO();
            return PartialView("_AddSubtitle", subtile);
        }
        [HttpGet]

        public ActionResult GetSubtitle(int id)
        {
            try
            {
                SubtitleDTO subt = new SubtitleDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var sbtitle = dbcontext.mblist_subtitle.Find(id);
                    if (sbtitle != null)
                    {
                        subt.id = sbtitle.subtitle_key;
                        subt.Subtitle = sbtitle.subtitle_name;
                        return PartialView("_AddSubtitle", subt);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Subtitle not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult AddOrUpdateSubtitle(SubtitleDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_subtitle.Where(x => x.subtitle_name == dto.Subtitle).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Subtitle already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_subtitle sbtitle = new mblist_subtitle()
                                {
                                    subtitle_name = dto.Subtitle
                                };
                                dbcontext.mblist_subtitle.Add(sbtitle);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Subtitle added successfully" }, JsonRequestBehavior.AllowGet);
                            }


                        }
                        else
                        {

                            var data = dbcontext.mblist_subtitle.Find(dto.id);
                            if (data != null)
                            {
                                data.subtitle_name = dto.Subtitle;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Subtitle Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Subtitle is not found" }, JsonRequestBehavior.AllowGet);
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

                return Json(new { key = false, value = "Unable to save the Subtitle" }, JsonRequestBehavior.AllowGet); ;
            }

        }
        [HttpGet]
        public ActionResult SubtitleListing()
        {

            List<SubtitleDTO> subtitleList = new List<SubtitleDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {

                subtitleList = dbcontext.mblist_subtitle.AsEnumerable().OrderByDescending(x => x.subtitle_key).Select(x => new SubtitleDTO
                {

                    id = x.subtitle_key,
                    Subtitle = x.subtitle_name
                }).ToList();

            };
            return PartialView("_SubtitleListing", subtitleList);
        }

        public ActionResult DeleteSubtitle(int id)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {

                    var sbtitle = dbcontext.mblist_subtitle.Find(id);
                    if (sbtitle != null)
                    {
                        dbcontext.mblist_subtitle.Remove(sbtitle);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Subtitle deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Subtitle not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
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