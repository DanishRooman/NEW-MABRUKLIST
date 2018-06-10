using DataAccessLayer.DBContext;
using DataTransferObjects.Address;
using DataTransferObjects.Neighbourhood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class NeighbourhoodController : Controller
    {
        [Authorize]
        // GET: Neighbourhood
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddNeighbourhood()
        {

            NeighbourhoodDTO nbhood = new NeighbourhoodDTO();
            return PartialView("_AddNeighbourhood", nbhood);
        }
        [HttpGet]

        public ActionResult NeighbourhoodListing()
        {

            List<NeighbourhoodDTO> NeighbourhoodList = new List<NeighbourhoodDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                NeighbourhoodList = dbcontext.mblist_neighborhoods.AsEnumerable().OrderByDescending(x => x.neighborhoods_key).Select(x => new NeighbourhoodDTO
                {
                    id = x.neighborhoods_key,
                    Neighbourhood = x.neighborhoods_name

                }).ToList();
            };

            return PartialView("_NeighbourhoodListing", NeighbourhoodList);

        }

        [HttpPost]
        public ActionResult AddOrUpdateNeighbourhood(NeighbourhoodDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    if (dto.id == 0)
                    {
                        var data = dbcontext.mblist_neighborhoods.Where(x => x.neighborhoods_name == dto.Neighbourhood).FirstOrDefault();
                        if (data != null)
                        {
                            return Json(new { key = false, value = "Neighbourhood already exist" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            mblist_neighborhoods nbhood = new mblist_neighborhoods()
                            {
                                neighborhoods_name = dto.Neighbourhood
                            };
                            dbcontext.mblist_neighborhoods.Add(nbhood);
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Neighbourhood added successfully" }, JsonRequestBehavior.AllowGet);
                        }
                       
                    }
                    else
                    {
                        var data = dbcontext.mblist_neighborhoods.Find(dto.id);
                        if (data != null)
                        {
                            data.neighborhoods_name =dto.Neighbourhood;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "Neighbourhood updated successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = false, value = "Neighbourhood not found" }, JsonRequestBehavior.AllowGet);
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

                return Json(new { key = false, value = "Unable to save the Neighbourhood" }, JsonRequestBehavior.AllowGet); ;
            }

        }

        [HttpGet]
        public ActionResult GetNeighbourhood(int id)
        {

            try
            {
                NeighbourhoodDTO NBhood = new NeighbourhoodDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var NeighbourHD = dbcontext.mblist_neighborhoods.Find(id);
                    if (NeighbourHD != null)
                    {
                        NBhood.id = NeighbourHD.neighborhoods_key;
                        NBhood.Neighbourhood = NeighbourHD.neighborhoods_name;
                        return PartialView("_AddNeighbourhood", NBhood);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Neighbourhood not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Neighbourhood" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteNeighbourhood(int id) {

            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var add = dbcontext.mblist_neighborhoods.Find(id);
                    if (add != null)
                    {
                        dbcontext.mblist_neighborhoods.Remove(add);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Neighbourhood deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Neighbourhood not found" }, JsonRequestBehavior.AllowGet);
                    }
                };


            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Neighbourhood" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}