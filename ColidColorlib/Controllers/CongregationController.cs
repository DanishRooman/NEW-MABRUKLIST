using DataAccessLayer.DBContext;
using DataTransferObjects.Congregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
    public class CongregationController : Controller
    {
        // GET: Congregation
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCongregation()
        {

            CongregationDTO Cong = new CongregationDTO();
            return PartialView("_AddCongregation", Cong);
        }

        [HttpGet]

        public ActionResult CongregationListing()
        {


            List<CongregationDTO> congList = new List<CongregationDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                congList = dbcontext.mblist_congregation.AsEnumerable().OrderByDescending(x => x.congregation_key).Select(x => new CongregationDTO
                {
                    id = x.congregation_key,
                    congregation = x.congregation_name

                }).ToList();
            };

            return PartialView("_CongregationListing", congList);

        }
        [HttpPost]

        public ActionResult AddOrUpdateCongregation(CongregationDTO dto)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {

                            var data = dbcontext.mblist_congregation.Where(x => x.congregation_name == dto.congregation).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Congregation already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_congregation congr = new mblist_congregation()
                                {
                                    congregation_name = dto.congregation

                                };
                                dbcontext.mblist_congregation.Add(congr);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Congregation added successfully" }, JsonRequestBehavior.AllowGet);
                            }


                        }
                        else
                        {
                            var data = dbcontext.mblist_congregation.Find(dto.id);
                            if (data != null)
                            {
                                data.congregation_name = dto.congregation;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Congregation updated successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Congregation not found" }, JsonRequestBehavior.AllowGet);
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


                return Json(new { key = false, value = "Unable to save the Congregation" }, JsonRequestBehavior.AllowGet); ;

            }


        }
        [HttpGet]
        public ActionResult GetCongregation(int id)
        {

            try
            {
                CongregationDTO congr = new CongregationDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var con = dbcontext.mblist_congregation.Find(id);
                    if (con != null)
                    {
                        congr.id = con.congregation_key;
                        congr.congregation = con.congregation_name;
                        return PartialView("_AddCongregation", congr);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Congregation not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the address" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCongregation(int id) {

            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var cong = dbcontext.mblist_congregation.Find(id);
                    if (cong != null)
                    {
                        dbcontext.mblist_congregation.Remove(cong);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Congregation deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Congregation not found" }, JsonRequestBehavior.AllowGet);
                    }
                };


            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Congregation" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}