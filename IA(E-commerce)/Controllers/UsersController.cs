using IA_E_commerce_.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IA_E_commerce_.Controllers
{
    public class UsersController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
         
        public UsersController ()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }


        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
     
       public ActionResult updateProfile ()
        {
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                ApplicationUser currentUser = UserManager.FindById(id);
                //var user = UserManager.FindById(User.Identity.GetUserId());
                return View(currentUser);
            }
            else
                return View("updateProfile") ;
        }

    }
}