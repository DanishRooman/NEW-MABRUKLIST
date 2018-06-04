using DataAccessLayer.DBContext;
using DataTransferObjects.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddGroup()
        {
            GroupDTO Group = new GroupDTO();
            return PartialView("AddGroup", Group);
        }
        [HttpGet]
        public ActionResult GroupListing()
        {
            List<GroupDTO> groupList = new List<GroupDTO>();
            using (MABRUKLISTEntities dbcontext=new MABRUKLISTEntities())
            {
                groupList = dbcontext.mblist_group.AsEnumerable().OrderByDescending(x => x.group_key).Select(x => new GroupDTO
                {
                    id = x.group_key,
                    Group = x.group_name

                }).ToList();
            };
            return PartialView("_CategoryListing", groupList);
        }


        //insert process
        [HttpPost]
        public ActionResult AddOrUpdateGroup(GroupDTO dto)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities()) {

                    mblist_group group = new mblist_group()
                    {
                        group_name = dto.Group
                    };
                    dbcontext.mblist_group.Add(group);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "Group added successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to save the Group" }, JsonRequestBehavior.AllowGet); ;
            }
           
        }
    }
}