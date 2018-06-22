using DataAccessLayer.DBContext;
using DataTransferObjects.Category;
using DataTransferObjects.Event;
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
       
      
    }
}