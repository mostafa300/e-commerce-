using IA_E_commerce_.Models;
using IA_E_commerce_.ModelView;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IA_E_commerce_.Controllers

{   [Authorize]
    public class UsersController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        private ApplicationDbContext _context;

        public UsersController ()
        {
            _context = new ApplicationDbContext();
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        

       
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

            // GET: Users
        public ActionResult Index()
        {
            
            if (User.IsInRole("Customer"))
            {
                CustomerPosts customer = new CustomerPosts((string)(Session["id"]), 1); //get Customer's Posts
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


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


        public ActionResult Save(ApplicationUser user, HttpPostedFileBase image1)
        {
            var UserEd = new ApplicationUser();
            UserEd = _context.Users.Single(c => c.Id == user.Id);

            UserEd.lastName = user.lastName;
            UserEd.jobDescription = user.jobDescription;
            UserEd.phone = user.phone;
            UserEd.firstName = user.firstName;
            UserEd.Email = user.Email;
            UserEd.ImageValue = new byte[image1.ContentLength];
            image1.InputStream.Read(UserEd.ImageValue, 0, image1.ContentLength);
            _context.SaveChanges();
           // return View();
            // return View("updateProfile"); 
            return RedirectToAction("updateProfile", "Users");
            
        }

        public ActionResult About()
        {

            if (User.IsInRole("Customer"))
            {
                return View();
            }
            return HttpNotFound();
        }





    }
}