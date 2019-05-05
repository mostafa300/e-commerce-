using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA_E_commerce_.Models;
using IA_E_commerce_.ModelView;

namespace IA_E_commerce_.Controllers
{   [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DefultHome()
        {
            AllPosts item = new AllPosts(false , false);
            Popup item1 = new Popup();
            //Popup item2 = new Popup();
            item1.Posts = item;
            return View(item1);
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

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}