using DataAccessLayer.DBContext;
using DataTransferObjects.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
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
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                groupList = dbcontext.mblist_group.AsEnumerable().OrderByDescending(x => x.group_key).Select(x => new GroupDTO
                {
                    id = x.group_key,
                    Group = x.group_name

                }).ToList();
            };
            return PartialView("_GroupListing", groupList);
        }


        //insert process
        [HttpPost]
        public ActionResult AddOrUpdateGroup(GroupDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                    {
                        if (dto.id == 0)
                        {
                            var data = dbcontext.mblist_group.Where(x => x.group_name == dto.Group).FirstOrDefault();
                            if (data != null)
                            {
                                return Json(new { key = false, value = "Group already exist" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                mblist_group group = new mblist_group()
                                {
                                    group_name = dto.Group
                                };
                                dbcontext.mblist_group.Add(group);
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Group added successfully" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            var data = dbcontext.mblist_group.Find(dto.id);
                            if (data != null)
                            {
                                data.group_name = dto.Group;
                                dbcontext.SaveChanges();
                                return Json(new { key = true, value = "Group Updated Successfully" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { key = false, value = "Group is not found" }, JsonRequestBehavior.AllowGet);
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

                return Json(new { key = false, value = "Unable to save the Group" }, JsonRequestBehavior.AllowGet); ;
            }

        }
        [HttpGet]
        public ActionResult GetGroup(int id)
        {
            try
            {
                GroupDTO Group = new GroupDTO();
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var group = dbcontext.mblist_group.Find(id);
                    if (group != null)
                    {
                        Group.id = group.group_key;
                        Group.Group = group.group_name;
                        return PartialView("AddGroup", Group);

                    }
                    else
                    {
                        return Json(new { key = false, value = "Group not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Group" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteGroup(int id)
        {

            try
            {
                using (MABRUKLISTEntities dbcontext=new MABRUKLISTEntities())
                {
                    var Group = dbcontext.mblist_group.Find(id);
                    if (Group != null)
                    {
                        dbcontext.mblist_group.Remove(Group);
                        dbcontext.SaveChanges();
                        return Json(new {key=true, value="Group deleted successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Group not Found its Deleted from data base!!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                return Json(new { key = false, value = "Unable to edit the Group" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}