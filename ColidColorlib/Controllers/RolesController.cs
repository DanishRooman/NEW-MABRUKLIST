using DataAccessLayer.DBContext;
using DataTransferObjects.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddRoles()
        {
            RolesDTO role = new RolesDTO();

            return PartialView("_AddRoles", role);
        }
        public ActionResult AddOrUpdateRoles(RolesDTO dto)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    Guid gid = Guid.NewGuid();
                    AspNetRoles rols = new AspNetRoles()
                    {
                        Id = gid.ToString(),
                        Name = dto.Roles
                    };
                    dbcontext.AspNetRoles.Add(rols);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "Roles added successfully" }, JsonRequestBehavior.AllowGet);
                };

            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to save the Address" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult RolesListing()
        {

            List<RolesDTO> RolesList = new List<RolesDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                RolesList = dbcontext.AspNetRoles.AsEnumerable().OrderByDescending(x => x.Id).Select(x => new RolesDTO
                {
                    id = x.Id,
                    Roles = x.Name

                }).ToList();
            };

            return PartialView("_RolesListing", RolesList);

        }
        public ActionResult DeleteRoles(int id)
        {

            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var rol = dbcontext.AspNetRoles.Find(id);
                    if (rol != null)
                    {
                        dbcontext.AspNetRoles.Remove(rol);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Roles deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Roles not found" }, JsonRequestBehavior.AllowGet);
                    }
                };





            }
            catch (Exception ex)
            {

                return Json(new { key = false, value = "Unable to edit the Roles" }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}