using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA_E_commerce_.ModelView;
using IA_E_commerce_.Models;
using Microsoft.AspNet.Identity;

namespace IA_E_commerce_.Controllers
{
    public class MTController : Controller
    {
        // GET: MT
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private ApplicationDbContext _context;
        public MTController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: MTL
        public ActionResult Index()
        {
            
            if (User.IsInRole("MT"))
            {
                string id = User.Identity.GetUserId();
                //to get All Current project & previose project
                List<PostContent> pre = new List<PostContent>();
                List<PostContent> cur = new List<PostContent>();
                List<PostContent> All = new List<PostContent>();
                MTLorMTPost MTLPostspre = new MTLorMTPost((string)(Session["id"]), true, "MT");
                MTLorMTPost MTLPostscur = new MTLorMTPost((string)(Session["id"]), false, "MT");
                All = _context.Posts.ToList();
                pre = MTLPostspre.MTPosts;
                cur = MTLPostscur.MTPosts;
                Session["preProject"] = pre.Count;
                Session["curProject"] = cur.Count;
                Session["Total"] = All.Count;
                MTLPostsAndTeams MTLPostsAndTeams = new MTLPostsAndTeams((string)(Session["id"]), false);
                return View(MTLPostsAndTeams);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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

        //save the update profile
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
            return RedirectToAction("updateProfile", "MD");

        }

        //information about MD
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Delivered()
        {
            if (User.IsInRole("MT"))
            {

                MTLorMTPost MTLPosts = new MTLorMTPost((string)(Session["id"]), true, "MT");



                return View(MTLPosts);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult NotDelivered()
        {
            if (User.IsInRole("MT"))
            {

                MTLorMTPost MTLPosts = new MTLorMTPost((string)(Session["id"]), false, "MT");



                return View(MTLPosts);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult StatisticalDiagrams()
        {
            if (User.IsInRole("MT"))
            {
                

                return View();
            }
            return HttpNotFound();
        }
        
        public ActionResult getSendedRequest()
        {
            if (User.IsInRole("MT"))
            {
                List<Notification> notification = new List<Notification>();
                notification = _context.Notification.Where(d => d.ToID == (string)(Session["id"])).Where(m => m.stutes != true).ToList();
                
                return View(notification);
                
            }
            return HttpNotFound();
        }


        //accept or refuse the requests 
        [HttpPost]
        public ActionResult JoinToProject(int postID, int accept = 0, int refuse = 0)
        {
            // Content(x.ToString());
            if (User.IsInRole("MT"))
            {
                List<Notification> All = new List<Notification>();
                All = _context.Notification.Where(m => m.postID == postID).ToList();
                if (accept == 1)
                {
                    Team InseretMTL = new Team();

                    InseretMTL.MTID = User.Identity.GetUserId();
                    InseretMTL.ProjectID = postID;


                    foreach (var item in All)
                    {
                        if (item.ToID == (string)(Session["id"]))
                        {
                            item.stutes = true;
                        }
                    }

                    _context.SaveChanges();

                    var s = _context.Team.Add(InseretMTL);
                    _context.SaveChanges();
                }
                if (refuse == 2)
                {

                    foreach (var item in All)
                    {
                        if (item.ToID == (string)(Session["id"]))
                        {
                            item.stutes = false;
                        }

                    }
                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "MT");
            }
            return HttpNotFound();
        }
        //Request to Leave
        public ActionResult RequestToLeave(int id)
        {
            if (User.IsInRole("MT"))
            {
                Notification MTLNoti = new Notification();  
                MTLNoti.FromID = User.Identity.GetUserId();
                MTLNoti.postID = id;

                PostContent test = _context.Posts.Find(id);

                MTLNoti.message = "Requested You To Leave Project: ";
                MTLNoti.createdOn = DateTime.Now;
                MTLNoti.ToID = test.MDID;
                MTLNoti.Type = 1;//request to leave
                var x = _context.Notification.Add(MTLNoti);
                _context.SaveChanges();

                return RedirectToAction("Index", "MT");
                //return View();
            }
            return HttpNotFound();
        }
    }
}