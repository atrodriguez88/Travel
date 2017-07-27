using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTravel.Models;
using WebTravel.ViewModels;

namespace WebTravel.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View(db.Users.ToList());
            }            
        }
        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = db.Users.Find(id);
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }

                ////Referencia Directa Insegura
                //var userId = User.Identity.GetUserId();
                //if (applicationUser.Id != userId)
                //{
                //    return RedirectToAction("Login", "Account");
                //}
                return View(applicationUser);
            }
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                ViewBag.Rol = new SelectList(roleManager.Roles.Select(r => r.Name).ToList());
                return View();
            }
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControlViewModel control)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.Values.ElementAt(0).Value.AttemptedValue != "" &&
                    ModelState.Values.ElementAt(1).Value.AttemptedValue != "" &&
                    ModelState.Values.ElementAt(2).Value.AttemptedValue != "")
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                        var user = new ApplicationUser()
                        {
                            UserName = control.username,
                        };
                        var result = userManager.Create(user, control.password);

                        if (result.Succeeded)
                        {
                            var result2 = userManager.AddToRole(userManager.FindByName(user.UserName).Id, control.Rol);
                            return RedirectToAction("Index");
                        }

                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                        ViewBag.Rol = new SelectList(roleManager.Roles.Select(r => r.Name).ToList());
                        return View(control);
                    }
                }
                return RedirectToAction("Create");
            }
            return RedirectToAction("Register", "Account");

        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var user = db.Users.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    ////Referencia Directa Insegura
                    //if (user.Id != User.Identity.GetUserId())
                    //{
                    //    return RedirectToAction("Login", "Account");
                    //}
                    else
                    {
                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                        var control = new ControlViewModel()
                        {
                            email = db.Users.Find(id).Email,
                            //password = db.Users.Find(id).PasswordHash,
                            username = db.Users.Find(id).UserName,
                            Rol = roleManager.FindById(db.Users.Find(id).Roles.FirstOrDefault().RoleId).Name
                        };
                        ViewBag.Rol = new SelectList(roleManager.Roles.Select(r => r.Name).ToList());
                        return View(control);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ControlViewModel control)
        {
            //db.Entry(control).State = EntityState.Modified;
            //db.SaveChanges();
            //**********************************************
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.Values.ElementAt(1).Value.AttemptedValue != "")
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        if (control.id != null)
                        {
                            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                            var user = userManager.FindById(control.id);
                            user.Email = control.email;
                            user.UserName = control.username;
                            roleManager.FindById(control.id).Name = control.Rol;
                            if (control.password != "")
                            {
                                user.PasswordHash = control.password;
                            }
                        }
                        return RedirectToAction("Index");
                    }
                }
                return View(control);
            }
            return RedirectToAction("Register", "Account");
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = db.Users.Find(id);
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                return View(applicationUser);
            }

        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = db.Users.Find(id);
                db.Users.Remove(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult AddRol()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRol(string roleName)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (roleName == null)
                {
                    return View();
                }
                using (var db = new ApplicationDbContext())
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                    if (roleManager.RoleExists(roleName))
                    {
                        ViewBag.Error = roleName + "was already in the system";
                        return View();
                    }
                    roleManager.Create(new IdentityRole(roleName));
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}