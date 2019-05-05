using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA_E_commerce_.ModelView;
using IA_E_commerce_.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IA_E_commerce_.Controllers
{
    public class MTLController : Controller
    {
        
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public MTLController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        private ApplicationDbContext _context;
        public MTLController()
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
            
            if (User.IsInRole("MTL"))
            {
                string id = User.Identity.GetUserId();
                //to get All Current project & previose project
                List<PostContent> pre = new List<PostContent>();
                List<PostContent> cur = new List<PostContent>();
                List<PostContent> All = new List<PostContent>();
                MTLorMTPost MTLPostspre = new MTLorMTPost((string)(Session["id"]), true, "MTL");
                MTLorMTPost MTLPostscur = new MTLorMTPost((string)(Session["id"]), false, "MTL");
                All = _context.Posts.ToList();
                pre = MTLPostspre.MTLPosts;
                cur = MTLPostscur.MTLPosts;
                Session["preProject"] = pre.Count;
                Session["curProject"] = cur.Count;
                Session["Total"] = All.Count;
                // CustomerPosts customer = new CustomerPosts((string)(Session["id"]), 0); //get Customer's Posts
                // return View(customer);

                MTLPostsAndTeams MTLPostsAndTeams = new MTLPostsAndTeams((string)(Session["id"]), true);
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
            if (User.IsInRole("MTL"))
            {

                MTLorMTPost MTLPosts = new MTLorMTPost((string)(Session["id"]), true, "MTL");



                return View(MTLPosts);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult NotDelivered()
        {
            if (User.IsInRole("MTL"))
            {

                MTLorMTPost MTLPosts = new MTLorMTPost((string)(Session["id"]), false, "MTL");



                return View(MTLPosts);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult RemoveMT(string id)
        {
            List<Team> X = new List<Team>();
            X = _context.Team.Where(c => c.MTID == id).ToList();
            foreach (var Team in X)
            {
                if (Team.MTID == id)
                {

                    Team.MTID = null;
                }

            }

            _context.SaveChanges();
            return RedirectToAction("Feedback", "MTL");
        }

        public ActionResult LeaveProject(int id)
        {
            string MTLid = User.Identity.GetUserId();
            List<Team> X = new List<Team>();
            X = _context.Team.Where(c => c.ProjectID == id).Where(m => m.MTLID == MTLid).ToList();
            foreach (var Team in X)
            {
                if (Team.MTLID == MTLid)
                {

                    Team.MTLID = null;
                }

            }

            _context.SaveChanges();
            return RedirectToAction("NotDelivered", "MTL");
        }
        public ActionResult Feedback()
        {
            
            if (User.IsInRole("MTL"))
            {
                var list = _context.Users.ToList();
                List<ApplicationUser> Customer = new List<ApplicationUser>();


                List<ApplicationUser> MT = new List<ApplicationUser>();

                List<Team> ALLTeams = new List<Team>();
                ALLTeams = _context.Team.ToList();
                MTLorMTPost MTL = new MTLorMTPost((string)(Session["id"]), false, "MTL");//get all MTL Teams and posts

                //foreach (var MTMember in MTL.MTLTeams)
                //{
                //    foreach (var user in list)
                //    {
                //        var isInRole = UserManager.IsInRole(user.Id, "MT");
                //        if (isInRole == true && user.Id == MTMember.MTID)
                //        {
                //            if (!MT.Contains(user))
                //                MT.Add(user);
                //        }
                //    }
                //}
                
                foreach(var MTMember in MTL.MTLTeams)
                {
                    foreach(var Team in ALLTeams) 
                    {
                        if (MTMember.ProjectID == Team.ProjectID && Team.MTID != null)
                        {
                            var user = _context.Users.Find(Team.MTID);
                            MT.Add(user);
                            
                        }
                            
                    }
                }
                return View(MT);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }


        public ActionResult FormFeedback(string id)
        {
            if (User.IsInRole("MTL"))
            {
               // string id = User.Identity.GetUserId();

                MTLandMTShared Projects = new MTLandMTShared(id , (string)(Session["id"]));
               
            
                return View(Projects);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult FeedbackAction(MTLandMTShared model)
        {
            if (User.IsInRole("MTL"))
            {
                
                FeedbackEvaluate FeedbackModel = new FeedbackEvaluate();
                FeedbackModel.Rate = model.FeedbackForm.Rate;
                FeedbackModel.MTID = model.MTID;
                FeedbackModel.MTLID = (string)(Session["id"]);
                FeedbackModel.Description = model.FeedbackForm.Description;
                 //return Content(model.MTID);
                FeedbackModel.FeedProjectID = model.FeedbackForm.FeedProjectID;


                ApplicationDbContext _context = new ApplicationDbContext();

                PostContent test = _context.Posts.Find(model.FeedbackForm.FeedProjectID);
                FeedbackModel.MDID = test.MDID;
               
                var x = _context.FeedbackEvaluate.Add(FeedbackModel);
                _context.SaveChanges();

                return RedirectToAction("Feedback" , "MTL");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult StatisticalDiagrams()
        {
            if (User.IsInRole("MTL"))
            {

                return View();
            }
            return HttpNotFound();
        }
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<Notification> GetNotifications()
        {
            var Notification = db.Notification.ToList();
            return Notification;
        }

        public ActionResult getSendedRequest()
        {//d=>d.FromID == (string)(Session["id"])
            if (User.IsInRole("MTL"))
            {
                var notification = GetNotifications().Where(d => d.ToID == (string)(Session["id"])).Where(m => m.stutes == null);
                return View(notification);
            }
            return HttpNotFound();
        }
      
       //accept or refuse the requests 
        [HttpPost]
        public ActionResult JoinToProject(int postID, int accept = 0,int refuse = 0)
        {
            // Content(x.ToString());
            if (User.IsInRole("MTL"))
            {
                List<Notification> All = new List<Notification>();
                All = _context.Notification.Where(m => m.postID == postID).ToList();
                if (accept == 1)
                {
                    Team InseretMTL = new Team();

                    InseretMTL.MTLID = User.Identity.GetUserId();
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
                if(refuse == 2)
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
              
                return RedirectToAction("Index", "MTL");
            }
            return HttpNotFound();
        }


        //get All MTs 
        public ActionResult AssignMTs(int id, string searching)//this id for ID project(int)
        {

            if (User.IsInRole("MTL"))
            {


                UsersNoti MTNoti = new UsersNoti();

                var list = _context.Users.ToList();

                List<ApplicationUser> MT = new List<ApplicationUser>();
                List<ApplicationUser> Final = new List<ApplicationUser>();

                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MT");
                    if (isInRole == true)
                    {
                        List<Notification> MTLNotification = new List<Notification>();
                        MTLNotification = _context.Notification.Where(d => d.ToID == user.Id).Where(m => m.postID == id).ToList();

                        foreach (var item in MTLNotification)
                        {
                            if (item.stutes != false)
                            {
                                Final.Add(user);

                            }

                        }
                    }
                }
                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MT");
                    if (isInRole == true)
                    {

                        if (!Final.Contains(user))
                            MT.Add(user);

                    }
                }

                if (!String.IsNullOrEmpty(searching))
                {
                    var MTLSearching = MT.Where(d => d.Email.Contains(searching)).ToList();
                    MTNoti.Users = MTLSearching;
                    return View(MTNoti);
                }
                Session["Pro"] = id;
                MTNoti.Users = MT;
                return View(MTNoti);

            }
            return HttpNotFound();

        }

        [HttpPost]
        public ActionResult AssignMTs(UsersNoti MTLNoti, string ToID)
        {
            if (User.IsInRole("MTL"))
            {
                MTLNoti.Notification.FromID = User.Identity.GetUserId();
                MTLNoti.Notification.postID = Convert.ToInt32(Session["Pro"]);

                PostContent test = _context.Posts.Find(MTLNoti.Notification.postID);

                MTLNoti.Notification.message = "Requested You To Join Project: ";
                MTLNoti.Notification.createdOn = DateTime.Now;
                MTLNoti.Notification.ToID = ToID;

                var x = _context.Notification.Add(MTLNoti.Notification);
                _context.SaveChanges();

                return RedirectToAction("AssignMTs", "MTL");
            }
            return HttpNotFound();

        }


    }
}