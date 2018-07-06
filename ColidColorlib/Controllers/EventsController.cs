using DataAccessLayer.DBContext;
using DataTransferObjects.Category;
using DataTransferObjects.ChooseEvent;
using DataTransferObjects.Congregation;
using DataTransferObjects.Event;
using DataTransferObjects.Group;
using DataTransferObjects.Neighbourhood;
using DataTransferObjects.Types;
using DataTransferObjects.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEvent()
        {
            EventDTO evnt = new EventDTO();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                //Category
                evnt.eventCategoryList = dbcontext.mblist_category.AsEnumerable().Select(x => new CategoryDto
                {
                    Id = x.cat_key,
                    Category = x.cat_name
                }).ToList();

                //Types
                evnt.eventTypesList = dbcontext.mblist_type.AsEnumerable().Select(x => new TypeDto
                {

                    id = x.type_key,
                    Type = x.type_name

                }).ToList();
            };
            return PartialView("_AddEvent", evnt);
        }

        [HttpGet]
        public ActionResult AddContact()
        {

            using (MABRUKLISTEntities dbcontaxt = new MABRUKLISTEntities())
            {

                //Group
                ViewBag.groups = dbcontaxt.mblist_group.AsEnumerable().Select(x => new SelectListItem
                {
                    Value = x.group_key.ToString(),
                    Text = x.group_name.ToString()

                }).ToList();
                //Congregation
                ViewBag.congregations = dbcontaxt.mblist_congregation.AsEnumerable().Select(x => new SelectListItem
                {
                    Value = x.congregation_key.ToString(),
                    Text = x.congregation_name
                }).ToList();
                //Nieghbourhood
                ViewBag.nieghbourhood = dbcontaxt.mblist_neighborhoods.AsEnumerable().Select(x => new SelectListItem
                {

                    Value = x.neighborhoods_key.ToString(),
                    Text = x.neighborhoods_name
                }).ToList();


            };
            return PartialView("_AddContact");
        }

        public ActionResult UserDetails(int? group, int? Congregations, int? neighbourhood)
        {
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                var allusers = dbcontext.mblist_user_info.AsEnumerable();
                if (group != null)
                {
                    allusers = allusers.Where(x => x.usr_group_key == group);
                }
                if (Congregations != null)
                {
                    allusers = allusers.Where(x => x.usr_congregation_key == Congregations);
                }
                if (neighbourhood != null)
                {
                    allusers = allusers.Where(x => x.usr_neighbourhood_key == neighbourhood);
                }
                var users = allusers.Select(x => new ContactUsersDto
                {
                    Users = x.usr_first_name + " " + x.usr_last_name,
                    Action = "<input type='checkbox' value='" + x.AspNetUsers.Id + "'>"
                }).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateEvents(EventDTO dtoEvent)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    //Following line is used to get logged in userid
                    string usrkey = User.Identity.GetUserId();
                    if (dtoEvent.id != 0)
                    {
                        var Data = dbcontext.mblist_events_detail.Find(dtoEvent.id);
                        if (Data != null)
                        {
                            Data.event_detail_category_key = dtoEvent.Category;
                            Data.event_detail_type_key = dtoEvent.EventFor;
                            Data.event_detail_title = dtoEvent.Title;
                            Data.event_detail_address = dtoEvent.Date;
                            Data.event_detail_discription = dtoEvent.Comment;
                            Data.event_detail_user_key = usrkey;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "event updated successfully", eventKey = Data.event_detail_key }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = false, value = "event event not found"}, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        mblist_events_detail evn = new mblist_events_detail()
                        {
                            
                            event_detail_category_key = dtoEvent.Category,
                            event_detail_type_key = dtoEvent.EventFor,
                            event_detail_title = dtoEvent.Title,
                            event_detail_address = dtoEvent.Address,
                            event_detail_date = Convert.ToDateTime(dtoEvent.Date),
                            event_detail_discription = dtoEvent.Comment,
                            event_detail_user_key = usrkey

                        };
                        dbcontext.mblist_events_detail.Add(evn);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "event added successfully", eventKey = evn.event_detail_key }, JsonRequestBehavior.AllowGet);


                    }


                };
            }
            catch (Exception ex)
            {

                return Json(new { key = false, value = "Unable to save the event" }, JsonRequestBehavior.AllowGet); ;
            }


        }

        //SubEvent Mathod
        [HttpPost]
        public ActionResult AddSubevent(EventDTO sbevntdto) {
            try
            {
                using (MABRUKLISTEntities dbcontext=new MABRUKLISTEntities()) 
                {
                    string sbevntkey = User.Identity.GetUserId();
                 
                    mblist_events_detail sbven = new mblist_events_detail()
                    {

                        event_detail_category_key = sbevntdto.Category,
                        event_detail_type_key = sbevntdto.EventFor,
                        event_detail_title = sbevntdto.Title,
                        event_detail_address = sbevntdto.Address,
                        event_detail_date = Convert.ToDateTime(sbevntdto.Date),
                        event_detail_discription = sbevntdto.Comment,

                        event_detail_user_key = 
                    };
                    dbcontext.mblist_events_detail.Add(sbven);
                    dbcontext.SaveChanges();
                    return Json(new { key = true, value = "Subevent added successfully", sbevntkey = sbven.event_detail_key }, JsonRequestBehavior.AllowGet);
              
                };
            }
            catch (Exception)
            {

                throw;
            }



         }
        

    }
}