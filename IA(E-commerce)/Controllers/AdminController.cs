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

        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //Ad
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


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
            
            if (User.IsInRole("Admin"))
            {

                AllPosts item = new AllPosts();
                return View(item);
            }
            return HttpNotFound();
        }

        public ActionResult CreateMD()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return HttpNotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateMDAction(RegisterViewModel model, HttpPostedFileBase image1) {
            if (User.IsInRole("Admin"))
            {

                if (ModelState.IsValid)
                {


                    var user = new ApplicationUser();

                    user.UserName = model.Email;
                    user.Email = model.Email;
                    user.firstName = model.firstName;
                    user.lastName = model.lastName;
                    user.phone = model.phone;
                    user.jobDescription = model.jobDescription;
                    user.ImageValue = new byte[image1.ContentLength];
                    image1.InputStream.Read(user.ImageValue, 0, image1.ContentLength);



                    var result = await UserManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {

                        await UserManager.AddToRoleAsync(user.Id, "MD");
                        
                        return RedirectToAction("Index", "Admin");
                    }

                }

                // If we got this far, something failed, redisplay form
                return View("CreateMD", model);

            }
            return HttpNotFound();

          
        }

        //Create MTL
        public ActionResult CreateMTL()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return HttpNotFound();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateMTLAction(RegisterViewModel model, HttpPostedFileBase image1)
        {


            if (ModelState.IsValid)
            {


                var user = new ApplicationUser();

                user.UserName = model.Email;
                user.Email = model.Email;
                user.firstName = model.firstName;
                user.lastName = model.lastName;
                user.phone = model.phone;
                user.jobDescription = model.jobDescription;
                user.ImageValue = new byte[image1.ContentLength];
                image1.InputStream.Read(user.ImageValue, 0, image1.ContentLength);


                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "MTL");

                    return RedirectToAction("Index", "Admin");
                }

            }

            // If we got this far, something failed, redisplay form
            return View("CreateMTL", model);




        }
        //Create MTL
        public ActionResult CreateMT()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return HttpNotFound();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateMTAction(RegisterViewModel model, HttpPostedFileBase image1)
        {


            if (ModelState.IsValid)
            {


                var user = new ApplicationUser();

                user.UserName = model.Email;
                user.Email = model.Email;
                user.firstName = model.firstName;
                user.lastName = model.lastName;
                user.phone = model.phone;
                user.jobDescription = model.jobDescription;
                user.ImageValue = new byte[image1.ContentLength];
                image1.InputStream.Read(user.ImageValue, 0, image1.ContentLength);


                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "MT");

                 
                    return RedirectToAction("Index", "Admin");
                }

            }

            // If we got this far, something failed, redisplay form
            return View("CreateMT", model);




        }


        //Create Customer
        public ActionResult CreateCustomer()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return HttpNotFound();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateCustomerAction(RegisterViewModel model, HttpPostedFileBase image1)
        {


            if (ModelState.IsValid)
            {


                var user = new ApplicationUser();

                user.UserName = model.Email;
                user.Email = model.Email;
                user.firstName = model.firstName;
                user.lastName = model.lastName;
                user.phone = model.phone;
                user.jobDescription = model.jobDescription;
                user.ImageValue = new byte[image1.ContentLength];
                image1.InputStream.Read(user.ImageValue, 0, image1.ContentLength);


                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "Customer");

                    return RedirectToAction("Index", "Admin");
                }

            }

            // If we got this far, something failed, redisplay form
            return View("CreateMT", model);




        }

        //Edit MD
        public ActionResult EditUser(String id)
        {
            if (User.IsInRole("Admin"))
            {
                var MD = _context.Users.SingleOrDefault(c => c.Id == id);
                /*RegisterViewModel User = new RegisterViewModel();
                User.Email = MD.Email;
                User.firstName = MD.firstName;
                User.lastName = MD.lastName;
                User.phone = MD.phone;*/
                return View(MD);
            }
            return HttpNotFound();
            
        }

        public ActionResult SaveAction(ApplicationUser user, HttpPostedFileBase image1)
        {
            if (User.IsInRole("Admin"))
            {
                var UserEd = new ApplicationUser();
                UserEd = _context.Users.Single(c => c.Id == user.Id);

                if(image1 != null)
                {
                    UserEd.lastName = user.lastName;
                    UserEd.jobDescription = user.jobDescription;
                    UserEd.phone = user.phone;
                    UserEd.firstName = user.firstName;
                    UserEd.Email = user.Email;
                    UserEd.ImageValue = new byte[image1.ContentLength];
                    image1.InputStream.Read(UserEd.ImageValue, 0, image1.ContentLength);
                }
                else
                {
                    UserEd.lastName = user.lastName;
                    UserEd.jobDescription = user.jobDescription;
                    UserEd.phone = user.phone;
                    UserEd.firstName = user.firstName;
                    UserEd.Email = user.Email;
                    //UserEd.ImageValue = new byte[image1.ContentLength];
                    //image1.InputStream.Read(UserEd.ImageValue, 0, image1.ContentLength);
                }
                _context.SaveChanges();
                // return View();
                 return View("EditUser"); 
                //return RedirectToAction("AllMD", "Admin");

            }
            return HttpNotFound();
        }

        ///View Add Project
        public ActionResult CreatePost()
        {
            

            if (User.IsInRole("Admin"))
            {

                var list = _context.Users.ToList();
                List<ApplicationUser> Customer = new List<ApplicationUser>();

                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "Customer");
                    if (isInRole == true)
                    {
                        Customer.Add(user);
                    }
                }
                AdminCreatePost data = new AdminCreatePost();
                data.Customer = Customer;
                

                return View(data);
            }
            return HttpNotFound();
        

        }

        //Create Post
        [HttpPost]
        public ActionResult createPostAction(AdminCreatePost x, List<HttpPostedFileBase> image1)
        {

            if (!ModelState.IsValid)
            {
                return View("creates");
            }

            AdminCreatePost model = new AdminCreatePost();

            if (image1 != null && image1.Count() <= 5 && x.pst.Title != null && x.pst.Description != null)
            {
                ApplicationDbContext _context = new ApplicationDbContext();

                model.pst.Description = x.pst.Description;
                model.pst.Title = x.pst.Title;


                model.pst.Relationid = x.pst.Relationid;


                _context.Posts.Add(model.pst);
                _context.SaveChanges();


                var id = int.Parse(_context.Posts.OrderByDescending(p => p.Id).Select(r => r.Id).First().ToString()); // gets post id



                int counter = 0;
                foreach (HttpPostedFileBase i in image1)
                {

                    model.img.Add(new Image { PostID = id });
                    model.img[counter].ImageValue = new byte[i.ContentLength];
                    i.InputStream.Read(model.img[counter].ImageValue, 0, i.ContentLength);
                    model.img[counter].Post = _context.Posts.FirstOrDefault(d => d.Id == id);

                    _context.Images.Add(model.img[counter]);
                    _context.SaveChanges();
                    counter++;
                }



                _context.Dispose();
            }
            else
            {
                return View("creates");
            }


            return RedirectToAction("index", "Admin");
            
        }
        //Delete User
        public ActionResult DeleteUser(string id )
        {
            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                
                AllTeams ALLTeams = new AllTeams();
               
                List<PostContent> MDTeams = new List<PostContent>();
                List<PostContent> MDPosts = new List<PostContent>();
                MDPosts = _context.Posts.ToList();
                var isInRole = UserManager.IsInRole(id, "MD");
                var isInRoleMTL = UserManager.IsInRole(id, "MTL");
                var isInRoleMT = UserManager.IsInRole(id, "MT");
                var isInRoleCustomer = UserManager.IsInRole(id, "Customer");
                if (isInRole == true)
                {
                    //if the user is MD Delete his ID from Project
                    foreach (var item in MDPosts)
                    {
                        if (item.MDID == id)
                        {
                            PostContent x = _context.Posts.Find(item.Id);
                            MDTeams.Add(x);


                            x.MDID = null;
                            x.assign = false;
                            _context.SaveChanges();
                        }
                    }
                    //if the user is MD Delte ALL his Teams
                    foreach (var item in MDTeams)
                    {
                        foreach (var x in ALLTeams.Team)
                        {
                            if (item.Id == x.ProjectID)
                            {
                                Team xteam = _context.Team.Find(x.id);
                                var T = _context.Team.Remove(xteam);
                                _context.SaveChanges();
                            }
                        }
                    }
                }
                
                    //if the user is MTL Or MT
                    foreach (var item in ALLTeams.Team)
                    {
                        if (item.MTID == id)
                        {
                            Team xteam = _context.Team.Find(item.id);
                            xteam.MTID = null;
                            
                            _context.SaveChanges();
                        }
                        if (item.MTLID == id)
                        {
                            Team xteam = _context.Team.Find(item.id);
                            xteam.MTLID = null;

                            _context.SaveChanges();
                        }

                    }

                //if MTL = null && MT = null then remove it
                foreach(var item in ALLTeams.Team)
                {
                    if(item.MTID == null && item.MTLID == null)
                    {
                        Team xteam = _context.Team.Find(item.id);
                        var M = _context.Team.Remove(xteam);
                        _context.SaveChanges();
                    }
                }

                //if the user is customer 
                var isInRolee = UserManager.IsInRole(id, "Customer");
                if (isInRolee == true)
                {
                    List<PostContent> Posts = new List<PostContent>();
                    Posts = _context.Posts.Where(m => m.Relationid == id).ToList();
                    foreach(var item in Posts)
                    {
                        var x = _context.Posts.Find(item.Id);
                        var delete = _context.Posts.Remove(x);
                        _context.SaveChanges();
                    }

                }

                ApplicationUser user = _context.Users.Find(id);
                var Z = _context.Users.Remove(user);
                _context.SaveChanges();


                if (isInRoleCustomer == true)
                {
                   return RedirectToAction("AllCustomer", "Admin");
                }
                if (isInRole == true)
                {
                    return RedirectToAction("AllMD", "Admin");
                }
                if (isInRoleMTL == true)
                {
                    return RedirectToAction("AllMTL", "Admin");
                }
                if (isInRoleMT == true)
                {
                    return RedirectToAction("AllMT", "Admin");
                }
            }
            return HttpNotFound();
        }
        //Show All MD
        public ActionResult AllMD()
        {

            if (User.IsInRole("Admin"))
            {

                var list = _context.Users.ToList();
                List<ApplicationUser> Customer = new List<ApplicationUser>();
                
                
                    List<ApplicationUser> MD = new List<ApplicationUser>();
                    foreach (var user in list)
                    {
                        var isInRole = UserManager.IsInRole(user.Id, "MD");
                        if (isInRole == true)
                        {
                            MD.Add(user);
                        }
                    }


                    return View(MD);

                
                
            }
            return HttpNotFound();

            
        }

        public ActionResult AllMTL()
        {

            if (User.IsInRole("Admin"))
            {

                var list = _context.Users.ToList();
                List<ApplicationUser> Customer = new List<ApplicationUser>();


                List<ApplicationUser> MTL = new List<ApplicationUser>();
                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MTL");
                    if (isInRole == true)
                    {
                        MTL.Add(user);
                    }
                }


                return View(MTL);



            }
            return HttpNotFound();


        }

        public ActionResult AllMT()
        {

            if (User.IsInRole("Admin"))
            {

                var list = _context.Users.ToList();
                List<ApplicationUser> Customer = new List<ApplicationUser>();


                List<ApplicationUser> MT = new List<ApplicationUser>();
                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MT");
                    if (isInRole == true)
                    {
                        MT.Add(user);
                    }
                }


                return View(MT);



            }
            return HttpNotFound();


        }

        public ActionResult AllCustomer()
        {

            if (User.IsInRole("Admin"))
            {

                var list = _context.Users.ToList();
                List<ApplicationUser> Customer = new List<ApplicationUser>();


                List<ApplicationUser> MT = new List<ApplicationUser>();
                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "Customer");
                    if (isInRole == true)
                    {
                        MT.Add(user);
                    }
                }


                return View(MT);



            }
            return HttpNotFound();
        }

        //Edit Post Like PostDetils But I create it to go to another view "Edit Post"
        public ActionResult EditPostDetails(int id)
        {
            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();

                PostImages x = new PostImages();
                x.pst.Id = id;
                x.pst = _context.Posts.Find(id);
                x.img = _context.Images.Where(s => s.PostID == id).ToList();
                return View(x);
            }
            return HttpNotFound();
        }

        public ActionResult EditPostImages(int id)
        {
            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();

                PostImages x = new PostImages();
                x.pst.Id = id;
                x.pst = _context.Posts.Find(id);
                x.img = _context.Images.Where(s => s.PostID == id).ToList();
                return View(x);
            }
            return HttpNotFound();
        }

        //UpdatePost Function
        public ActionResult UpdatePost(PostAndImage Post)
        {
            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                var PostInDb = _context.Posts.Single(c => c.Id == Post.pst.Id);
                PostInDb.Title = Post.pst.Title;
                PostInDb.Description = Post.pst.Description;

                _context.SaveChanges();


                return RedirectToAction("index", "Admin");

            }

            return HttpNotFound();

        }


        public ActionResult AddImagesForPost(PostImages x, List<HttpPostedFileBase> image1)
        {
            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                var id = int.Parse(_context.Posts.OrderByDescending(p => p.Id).Select(r => r.Id).First().ToString()); // gets post id
                x.img = _context.Images.Where(s => s.PostID == id).ToList();
                int NumberOfImages = x.img.Count() + image1.Count();
                if (image1 != null && NumberOfImages <= 5)
                {
                    int counter = 0;
                    foreach (HttpPostedFileBase i in image1)
                    {

                        x.img.Add(new Image { PostID = id });
                        x.img[counter].ImageValue = new byte[i.ContentLength];
                        i.InputStream.Read(x.img[counter].ImageValue, 0, i.ContentLength);
                        x.img[counter].Post = _context.Posts.FirstOrDefault(d => d.Id == id);

                        _context.Images.Add(x.img[counter]);
                        _context.SaveChanges();
                        counter++;
                    }
                }

                return RedirectToAction("index", "Admin");

            }

            return HttpNotFound();
        }

        //DeletePost Function 
        public ActionResult DeletePost(int id)
        {

            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                PostContent Post = _context.Posts.Find(id);
                //AllImages Images = new AllImages();
                /*foreach(var item in Images.img)
                {
                    if(id == item.PostID)
                    {
                        var Delete = _context.Images.Single(c => c.PostID == item.PostID);
                        var d = _context.Images.Remove(Delete);
                        _context.SaveChanges();
                    }
                }*/
                var s = _context.Posts.Remove(Post);
                _context.SaveChanges();


                return RedirectToAction("index", "Admin");

            }

            return HttpNotFound();

        }

        //Delete Speceic Images From Post Based on Customer Desire
        public ActionResult DeleteImage(int id)
        {

            if (User.IsInRole("Admin"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                AllImages Imgs = new AllImages();
                foreach (var img in Imgs.img)
                {
                    if (img.Id == id)
                    {
                        Image x = _context.Images.Find(id);
                        var Delete = _context.Images.Remove(x);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("index", "Admin");

            }

            return HttpNotFound();

        }

        //information about MD
        public ActionResult About()
        {
            return View();
        }
        public ActionResult DeliveredProject()
        {
            AllPosts item = new AllPosts(true, false);
            
            return View(item);
        }
        public ActionResult NotDeliveredProject()
        {
            AllPosts item = new AllPosts(true, true);
            
            return View(item);
        }
        public ActionResult PostDetails(int id)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            PostImages x = new PostImages();
            x.pst.Id = id;
            x.pst = _context.Posts.Find(id);
            x.img = _context.Images.Where(s => s.PostID == id).ToList();
            return View(x);

        }

        //show profile
        public ActionResult UpdateProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                ApplicationUser currentUser = UserManager.FindById(id);
                //var user = UserManager.FindById(User.Identity.GetUserId());
                return View(currentUser);
            }
            else

                return View("updateProfile");
        }

      


        public ActionResult Save(ApplicationUser user , HttpPostedFileBase image1)
        {
            if (User.IsInRole("Admin"))
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
                return RedirectToAction("updateProfile", "Admin");
            }
            return HttpNotFound();
        }

    }
}