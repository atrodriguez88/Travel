using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    [RoutePrefix("Webapi")]
    public class WebApiController : Controller
    {
        // GET: WebApi
        
        [Route("~/")] // Domain
        [Route("")] // Domain/Controller
        [Route("Indes")] // Domain/Controller/Indes
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

    }
}
