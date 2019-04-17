using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA_E_commerce_.Models;
using System.Data.Entity;
using IA_E_commerce_.ModelView;
namespace IA_E_commerce_.Controllers
{
    public class CreatePostController : Controller
    {
        
        private PostAndImage _context;
       
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
           
            if (!ModelState.IsValid)
            {
                return View("creates");
            }

            PostImages model = new PostImages();

            if (image1 != null && image1.Count() <= 5 && x.pst.Title != null && x.pst.Description != null)
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
            }
            else
            {
                return View("creates");
            }

            
               return RedirectToAction("index", "Users");

            

        }


        //show all post which in database
        public ActionResult show()
        {
           
            AllPosts item = new AllPosts();
            return View(item);
        }

        //show all posts of user
        public ActionResult UserPosts()
        {
           
            CustomerPosts customer = new CustomerPosts((string)(Session["id"]), 0); //get Customer's Posts
            return View(customer);
        }

        public ActionResult PostDetails(int id)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
       
            PostImages x = new PostImages();
            x.pst.Id = id;
            x.pst=_context.Posts.Find(id);
            x.img = _context.Images.Where(s => s.PostID == id).ToList();
           return View(x);
               
        }

        //get Customer's Posts Delivered

        public ActionResult Delivered() 
        {

            CustomerPosts customer = new CustomerPosts(Session["id"].ToString(), 1); //get Customer's Posts
            return View(customer);
        }
        //get Customer's Posts Not Delivered
        public ActionResult NotDelivered() 
        {

            CustomerPosts customer = new CustomerPosts(Session["id"].ToString(), 2); //get Customer's Posts
            return View(customer);
        }

        //Edit Post Like PostDetils But I create it to go to another view "Edit Post"
        public ActionResult EditPostDetails(int id)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            PostImages x = new PostImages();
            x.pst.Id = id;
            x.pst = _context.Posts.Find(id);
            x.img = _context.Images.Where(s => s.PostID == id).ToList();
            return View(x);
        }

        public ActionResult EditPostImages(int id)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            PostImages x = new PostImages();
            x.pst.Id = id;
            x.pst = _context.Posts.Find(id);
            x.img = _context.Images.Where(s => s.PostID == id).ToList();
            return View(x);
        }

        //UpdatePost Function
        public ActionResult UpdatePost(PostAndImage Post)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            var PostInDb = _context.Posts.Single(c => c.Id == Post.pst.Id);
            PostInDb.Title = Post.pst.Title;
            PostInDb.Description = Post.pst.Description;

            _context.SaveChanges();

            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("index", "Users");

            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "Admin");


            }
            return HttpNotFound();

        }


        public ActionResult AddImagesForPost(PostImages x, List<HttpPostedFileBase> image1)
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
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("index", "Users");

            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "Admin");


            }
            return HttpNotFound();
        }

        //DeletePost Function 
        public ActionResult DeletePost(int id)
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

            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("index", "Users");

            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "Admin");


            }
            return HttpNotFound();

        }

        //Delete Speceic Images From Post Based on Customer Desire
        public ActionResult DeleteImage(int id)
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

            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("index", "Users");

            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "Admin");


            }
            return HttpNotFound(); 

        }



    }
}