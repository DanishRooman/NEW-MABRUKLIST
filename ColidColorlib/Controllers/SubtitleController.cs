using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
    public class SubtitleController : Controller
    {
        // GET: Subtitle
        public ActionResult Index()
        {
            return View();
        }
    }
}