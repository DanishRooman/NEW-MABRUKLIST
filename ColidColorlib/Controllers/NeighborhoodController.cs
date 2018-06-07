using DataAccessLayer.DBContext;
using DataTransferObjects.Neighborhood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class NeighborhoodController : Controller
    {
        //public object addressList { get; private set; }

        // GET: Neighborhood
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddNeighborhood()
        {

            NeighborhoodDTO Nbhd = new NeighborhoodDTO();
            return PartialView("_AddNeighborhood", Nbhd);
        }
        [HttpGet]

        public ActionResult GetNeighborhood(int id)
        {
            try
            {
                NeighborhoodDTO nbh = new NeighborhoodDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var Neighbr = dbcontext.mblist_neighborhoods.Find(id);
                    if (Neighbr != null)
                    {
                        nbh.id = Neighbr.neighborhoods_key;
                        nbh.Neighborhood = Neighbr.neighborhoods_name;
                        return PartialView("_AddNeighborhood", nbh);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Neighborhood not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Neighborhood" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]

        public ActionResult NeighborhoodListing()
        {


            List<NeighborhoodDTO> NbList = new List<NeighborhoodDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                NbList = dbcontext.mblist_neighborhoods.AsEnumerable().OrderByDescending(x => x.neighborhoods_key).Select(x => new NeighborhoodDTO
                {
                    id = x.neighborhoods_key,
                    Neighborhood = x.neighborhoods_name

                }).ToList();
            };

            return PartialView("_NeighborhoodListing", NbList);
        }

        [HttpPost]
        public ActionResult AddOrUpdateNeighborhood(NeighborhoodDTO dto)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_neighborhoods.Where(x => x.neighborhoods_name == dto.Neighborhood).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Neighborhood already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_neighborhoods neighbor = new mblist_neighborhoods()
                                {
                                    neighborhoods_name = dto.Neighborhood
                                };
                                dbcontext.mblist_neighborhoods.Add(neighbor);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Neighborhood added successfully" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        {

                            var data = dbcontext.mblist_neighborhoods.Find(dto.id);
                            if (data != null)
                            {
                                data.neighborhoods_name = dto.Neighborhood;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Neighborhood updated successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Neighborhood not found" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { key = false, value = "Unable to save the Neighborhood" }, JsonRequestBehavior.AllowGet); ;
            }
        }

        public ActionResult DeleteNeighborhood(int id)
        {

            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var nb = dbcontext.mblist_neighborhoods.Find(id);
                    if (nb != null)
                    {
                        dbcontext.mblist_neighborhoods.Remove(nb);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Neighborhood deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Neighborhood not found" }, JsonRequestBehavior.AllowGet);
                    }
                };


            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Neighborhood" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}