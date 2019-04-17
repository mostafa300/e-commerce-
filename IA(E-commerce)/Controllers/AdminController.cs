using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA_E_commerce_.ModelView;
using IA_E_commerce_.Models;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace IA_E_commerce_.Controllers
{
    public class AdminController : Controller
    {
        //Ad
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin

        public ActionResult Index()
        {

            AllPosts item = new AllPosts();
            return View(item);
        }

        public ActionResult CreateMD()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateMDAction(RegisterViewModel model) {
            
           
            if (ModelState.IsValid)
            {


                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    firstName = model.firstName,
                    lastName = model.lastName,
                    phone = model.phone,
                    jobDescription = model.jobDescription


                };

                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "MD");

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    return RedirectToAction("Index", "Admin");
                }
                
            }

            // If we got this far, something failed, redisplay form
            return View("CreateMD", model);



          
        }

        //Create MTL
        public ActionResult CreateMTL()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateMTLAction(RegisterViewModel model)
        {


            if (ModelState.IsValid)
            {


                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    firstName = model.firstName,
                    lastName = model.lastName,
                    phone = model.phone,
                    jobDescription = model.jobDescription


                };

                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "MTL");

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    return RedirectToAction("Index", "Admin");
                }

            }

            // If we got this far, something failed, redisplay form
            return View("CreateMTL", model);




        }
        //Create MTL
        public ActionResult CreateMT()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateMTAction(RegisterViewModel model)
        {


            if (ModelState.IsValid)
            {


                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    firstName = model.firstName,
                    lastName = model.lastName,
                    phone = model.phone,
                    jobDescription = model.jobDescription


                };

                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "MT");

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    return RedirectToAction("Index", "Admin");
                }

            }

            // If we got this far, something failed, redisplay form
            return View("CreateMT", model);




        }
    }
}