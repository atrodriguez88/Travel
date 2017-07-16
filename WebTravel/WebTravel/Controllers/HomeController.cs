using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTravel.ViewModels;
using ModelTravel;

namespace WebTravel.Controllers
{
    [RoutePrefix("Travel")]
    public class HomeController : Controller
    {
        [Route("~/")] // Domain
        [Route("")] // Domain/Controller
        [Route("Index")] // Domain/Controller/Index
        public ActionResult Index()
        {
            return View();
        }
        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult FormContact(string InputEmail, string Textarea)
        {
            string rep = "";
            bool IsEmail = new EmailAddressAttribute().IsValid(InputEmail);
            if (IsEmail)
            {
                if (Textarea.Length > 10)
                {
                    rep = "Thank you for contact us";
                    return Json(rep);
                }
                else
                {
                    rep = "The comment must be more than 10 characters";
                    return Json(rep);
                }
            }
            else
            {
                rep = "Email is no correct";
                return Json(rep);
            }
        }
        [Route("Vacation")]
        //[OutputCache(CacheProfile = "", Duration = 120, Location = System.Web.UI.OutputCacheLocation.ServerAndClient, VaryByParam = "pagina")]
        public ActionResult Vacation(int pagina = 1)
        {
            try
            {
                using (DBTravelEntities db = new DBTravelEntities())
                {
                    var cantidadRegistrosPorPagina = 6; // parámetro
                    
                    //EF
                     var territoriesTest = db.Territories.Select(n => n.TerritoryDescription).ToList();

                    //DataBase View but return a List<VSelectTerritory>, change in IndexViewModel
                    //var territories = db.VSelectTerritories.ToList();

                    var territories = db.Territories.OrderBy(x => x.TerritoryID)
                        .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                        .Take(cantidadRegistrosPorPagina).ToList();
                    IndexViewModel view = new IndexViewModel();
                    view.Territories = territories;

                    view.PaginaActual = pagina;
                    view.TotalDeRegistros = db.Territories.Count();
                    view.RegistrosPorPagina = cantidadRegistrosPorPagina;

                    //RemoveCache cache
                    //string path = Url.Action("Vacation", "HomeController");
                    //Response.RemoveOutputCacheItem(path);


                    return View(view);
                }
            }
            catch (Exception e)
            {
                HandleErrorInfo error = new HandleErrorInfo(e, "HomeController", "Vacation");
                return View("Error", error);
            }
        }
        [Route("Places")]
        //[OutputCache(CacheProfile = "", Duration = 120, Location = System.Web.UI.OutputCacheLocation.ServerAndClient, VaryByParam = "pagina")]
        public ActionResult Places(int pagina = 1)
        {
            try
            {
                var cantidadRegistrosPorPagina = 6; // parámetro
                using (DBTravelEntities db = new DBTravelEntities())
                {
                    var supp = db.Suppliers.OrderBy(x => x.SupplierID)
                        .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                        .Take(cantidadRegistrosPorPagina).ToList();
                    IndexViewModel view = new IndexViewModel()
                    {
                        Suppliers = supp,
                        PaginaActual = pagina,
                        TotalDeRegistros = db.Suppliers.Count(),
                        RegistrosPorPagina = cantidadRegistrosPorPagina,

                    };
                    Random s = new Random(3);
                    return View(view);
                }
            }
            catch (Exception e)
            {
                HandleErrorInfo error = new HandleErrorInfo(e, "HomeController", "Places");
                return View("Error", error);
            }
        }
        public ActionResult PlaceDetail(int? id)
        {
            if (id != null)
            {
                using (DBTravelEntities db = new DBTravelEntities())
                {
                    Supplier supp = db.Suppliers.Find(id);
                    if (supp == null)
                    {
                        return HttpNotFound();
                    }
                    return View(supp);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}