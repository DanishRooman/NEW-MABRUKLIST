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
    }
}