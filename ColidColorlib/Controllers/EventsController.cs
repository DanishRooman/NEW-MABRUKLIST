using DataAccessLayer.DBContext;
using DataTransferObjects.Category;
using DataTransferObjects.ChooseEvent;
using DataTransferObjects.Congregation;
using DataTransferObjects.Event;
using DataTransferObjects.Group;
using DataTransferObjects.Neighbourhood;
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
        public ActionResult AddEvent() {
            EventDTO evnt = new EventDTO();
            using (MABRUKLISTEntities dbcontext=new MABRUKLISTEntities())
            {
                //Category
                evnt.eventCategoryList = dbcontext.mblist_category.AsEnumerable().Select(x => new CategoryDto
                {
                    Id = x.cat_key,
                    Category = x.cat_name
                }).ToList();
            };
            return PartialView("_AddEvent",evnt) ;
        }
        [HttpGet]

       public ActionResult AddContact()
        {
            
            using (MABRUKLISTEntities dbcontaxt=new MABRUKLISTEntities()) {

                //Group
                ViewBag.groups = dbcontaxt.mblist_group.AsEnumerable().Select(x => new SelectListItem
                {
                    Value=x.group_key.ToString(),
                    Text=x.group_name.ToString()

                }).ToList();
                //Congregation
                ViewBag.congregations = dbcontaxt.mblist_congregation.AsEnumerable().Select(x => new SelectListItem
                {
                    Value = x.congregation_key.ToString(),
                    Text=x.congregation_name
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

      
    }
}