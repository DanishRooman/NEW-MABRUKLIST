using DataAccessLayer.DBContext;
using DataTransferObjects.Address;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAddress()
        {
            AddressDTO Address = new AddressDTO();
            return PartialView("_AddAddress", Address);
        }
        [HttpGet]
        public ActionResult AddressListing()
        {
            List<AddressDTO> addressList = new List<AddressDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                addressList = dbcontext.mblist_address.AsEnumerable().OrderByDescending(x => x.address_key).Select(x => new AddressDTO
                {
                    id = x.address_key,
                    Address = x.address_name

                }).ToList();
            };

            return PartialView("_AddressListing", addressList);
        }


        [HttpPost]
        public ActionResult AddOrUpdateAddress(AddressDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_address.Where(x => x.address_name == dto.Address).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Address already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_address adress = new mblist_address()
                                {
                                    address_name = dto.Address
                                };
                                dbcontext.mblist_address.Add(adress);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Address added successfully" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        {
                            var data = dbcontext.mblist_address.Find(dto.id);
                            if (data != null)
                            {
                                data.address_name = dto.Address;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Address updated successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Address not found" }, JsonRequestBehavior.AllowGet);
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

                return Json(new { key = false, value = "Unable to save the Address" }, JsonRequestBehavior.AllowGet); ;
            }

        }
        [HttpGet]
        public ActionResult GetAddress(int id)
        {
            try
            {
                AddressDTO adrs = new AddressDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var address = dbcontext.mblist_address.Find(id);
                    if (address != null)
                    {
                        adrs.id = address.address_key;
                        adrs.Address = address.address_name;
                        return PartialView("_AddAddress", adrs);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Address not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the address" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteAddress(int id)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var add = dbcontext.mblist_address.Find(id);
                    if (add != null)
                    {
                        dbcontext.mblist_address.Remove(add);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Address deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Address not found" }, JsonRequestBehavior.AllowGet);
                    }
                };


            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Address" }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}