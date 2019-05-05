using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA_E_commerce_.Models;
using System.Data.Entity;
using IA_E_commerce_.ModelView;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace IA_E_commerce_.Controllers
{
    public class CreatePostController : Controller
    {
        private ApplicationDbContext _context;
        public CreatePostController()
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


        public CreatePostController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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



        // return view of form add post
        public ActionResult creates()
        {
            //Image img = new Image();
            if (User.IsInRole("Customer"))
            {
                PostImages data = new PostImages();
                return View(data);
            }
            return HttpNotFound();
        }

        //save the create of new post

        [HttpPost]
        public ActionResult create(PostImages x, List<HttpPostedFileBase> image1)
        {
            if (User.IsInRole("Customer"))
            {
                /*if (!ModelState.IsValid)
                {
                    return View("creates");
                }*/

                if (x.pst.Title == null || x.pst.Description == null)
                {
                    return View("creates");
                }
                else
                {
                    PostImages model = new PostImages();

                    if (image1.Count() <= 5)
                    {
                        ApplicationDbContext _context = new ApplicationDbContext();

                        model.pst.Description = x.pst.Description;
                        model.pst.Title = x.pst.Title;


                        model.pst.Relationid = Convert.ToString(Session["id"]);


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
                        return RedirectToAction("index", "Users");
                    }
                    else
                    {
                        return View("creates");
                    }


                    

                }
            }
                  
            return HttpNotFound();

        }


        //show all post which in database
        public ActionResult show()
        {
            if (User.IsInRole("Customer"))
            {
                AllPosts item = new AllPosts();
                return View(item);
            }
            return HttpNotFound();
        }

        //show all posts of user
        public ActionResult UserPosts()
        {
            if (User.IsInRole("Customer"))
            {
                CustomerPosts customer = new CustomerPosts((string)(Session["id"]),1); //get Customer's Posts
                return View(customer);
            }
            return HttpNotFound();
        }

        public ActionResult PostDetails(int id)
        {
            if (User.IsInRole("Customer"))
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

        //get Customer's Posts Delivered

        public ActionResult Delivered() 
        {
            if (User.IsInRole("Customer"))
            {

                CustomerPosts customer = new CustomerPosts(Session["id"].ToString(), 2); //get Customer's Posts
                return View(customer);
            }
            return HttpNotFound();
        }
        //get Customer's Posts Not Delivered
        public ActionResult NotDelivered() 
        {
            if (User.IsInRole("Customer"))
            {

                CustomerPosts customer = new CustomerPosts(Session["id"].ToString(), 3); //get Customer's Posts
                return View(customer);
            }
            return HttpNotFound();
        }

        //Edit Post Like PostDetils But I create it to go to another view "Edit Post"
        public ActionResult EditPostDetails(int id)
        {
            if (User.IsInRole("Customer"))
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
            if (User.IsInRole("Customer"))
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
            if (User.IsInRole("Customer"))
            {
                    ApplicationDbContext _context = new ApplicationDbContext();
                var PostInDb = _context.Posts.Single(c => c.Id == Post.pst.Id);
                PostInDb.Title = Post.pst.Title;
                PostInDb.Description = Post.pst.Description;

                _context.SaveChanges();

            
                    return RedirectToAction("index", "Users");

            }
            
            return HttpNotFound();

        }


        public ActionResult AddImagesForPost(PostImages x, List<HttpPostedFileBase> image1)
        {
            if (User.IsInRole("Customer"))
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
            
                    return RedirectToAction("index", "Users");

            }
            
            return HttpNotFound();
        }

        //DeletePost Function 
        public ActionResult DeletePost(int id)
        {
            if (User.IsInRole("Customer"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                PostContent Post = _context.Posts.Find(id);
                if (Post.assign == false)
                {
                    var s = _context.Posts.Remove(Post);
                    _context.SaveChanges();
                    return RedirectToAction("index", "Users");
                }
                else
                {
                    return Content("you can not delete this project");
                }



            }

            return HttpNotFound();
        }

            //Delete Speceic Images From Post Based on Customer Desire
        public ActionResult DeleteImage(int id)
        {

            if (User.IsInRole("Customer"))
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

                    return RedirectToAction("index", "Users");

            }
            
            return HttpNotFound(); 

        }

        //Assign Project to MD
        public ActionResult AssignToMD(int id)
        {
            if (User.IsInRole("Customer"))
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                AdminCreatePost AssignPost = new AdminCreatePost();

                var list = _context.Users.ToList();
                List<ApplicationUser> MD = new List<ApplicationUser>();
                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MD");
                    if (isInRole == true)
                    {
                        MD.Add(user);
                    }
                }
                AssignPost.Customer = MD;
                AssignPost.pst.Id = id;

                return View(AssignPost);

            }

            return HttpNotFound();
        }

        public ActionResult AssignAction(AdminCreatePost MDAssgined)
        {
            if (User.IsInRole("Customer"))
            {

                PostContent AssginPost = _context.Posts.Find(MDAssgined.pst.Id);
                AssginPost.MDID = MDAssgined.pst.MDID;
                AssginPost.assign = true;
                _context.SaveChanges();
                return RedirectToAction("index", "Users");

            }

            return HttpNotFound();
        }




    }
}