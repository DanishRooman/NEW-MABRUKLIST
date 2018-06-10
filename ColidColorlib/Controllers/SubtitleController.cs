using DataAccessLayer.DBContext;
using DataTransferObjects.Subtitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
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

            SubtitleDTO SBtitle = new SubtitleDTO();
            return PartialView("_AddSubtitle", SBtitle);
        }

        [HttpGet]

        public ActionResult SubtitleListing()
        {

            List<SubtitleDTO> sbtitleList = new List<SubtitleDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                sbtitleList = dbcontext.mblist_subtitle.AsEnumerable().OrderByDescending(x => x.subtitle_key).Select(x => new SubtitleDTO
                {
                    id = x.subtitle_key,
                    subtitle = x.subtitle_name

                }).ToList();
            };

            return PartialView("_SubtitleListing", sbtitleList);

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
                            var data = dbcontext.mblist_subtitle.Where(x => x.subtitle_name == dto.subtitle).FirstOrDefault();

                            if (data != null)
                            {
                                return Json(new { key = false, value = "Subtitle already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_subtitle sbtitle = new mblist_subtitle()
                                {
                                    subtitle_name = dto.subtitle
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
                                data.subtitle_name = dto.subtitle;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Subtitle updated successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Subtitle not found" }, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetSubtitle(int id)
        {

            try
            {
                SubtitleDTO sbt = new SubtitleDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var SubT = dbcontext.mblist_subtitle.Find(id);
                    if (SubT != null)
                    {
                        sbt.id = SubT.subtitle_key;
                        sbt.subtitle = SubT.subtitle_name;
                        return PartialView("_AddSubtitle", sbt);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Subtitle not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Subtitle" }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult DeleteSubtitle(int id) {

            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var Del = dbcontext.mblist_subtitle.Find(id);
                    if (Del != null)
                    {
                        dbcontext.mblist_subtitle.Remove(Del);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Subtitle deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Subtitle not found" }, JsonRequestBehavior.AllowGet);
                    }
                };


            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Subtitle" }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}