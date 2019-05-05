using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using IA_E_commerce_.Models;
using IA_E_commerce_.ModelView;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IA_E_commerce_.Controllers
{
    public class MDController : Controller
    {
        private ApplicationDbContext _context;
        public MDController()
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


        public MDController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: MD which have all the post not assign and not deliverd
        public ActionResult Index()
        {
            if (User.IsInRole("MD"))
            {
                string id = User.Identity.GetUserId();
                //to get All Current project & previose project
                List<PostContent> pre = new List<PostContent>();
                List<PostContent> cur = new List<PostContent>();
                List<PostContent> All = new List<PostContent>();
                pre = _context.Posts.Where(m => m.MDID == id).Where(d => d.delivered == true).ToList();
                cur = _context.Posts.Where(m => m.MDID == id).Where(d => d.delivered == false).ToList();
                All = _context.Posts.ToList();
                
                Session["preProject"] = pre.Count;
                Session["curProject"] = cur.Count;
                Session["Total"] = All.Count;
                //var dump = ObjectDumper.Dump((Session["Photo"]);
                //return Content("this " + Total.ToString());
                AllPosts item = new AllPosts(false, false);
                return View(item);
                
            }
            return HttpNotFound();
        }
        //show profile
        public ActionResult UpdateProfile()
        {
            if (User.IsInRole("MD"))
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
            return HttpNotFound();
        }

        //save the update profile
        public ActionResult Save(ApplicationUser user, HttpPostedFileBase image1)
        {
            if (User.IsInRole("MD"))
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
            return HttpNotFound();
        }

        //information about MD
        public ActionResult About()
        {
            if (User.IsInRole("MD"))
            {
                return View();
            }
            return HttpNotFound();
        }

        //get MD's Posts Delivered

        public ActionResult Delivered()
        {
            if (User.IsInRole("MD"))
            {
                MDPost MD = new MDPost(Session["id"].ToString(), true);
                return View(MD);
            }
            return HttpNotFound();
        }
        //get MD's Posts Not Delivered
        public ActionResult NotDelivered()
        {
            if (User.IsInRole("MD"))
            {
                MDPost MD = new MDPost(Session["id"].ToString(), false);
                return View(MD);
            }
            return HttpNotFound();
        }

        //show post details
        public ActionResult PostDetails(int id)
        {
            if (User.IsInRole("MD"))
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


        //MD Assign aproject
        public ActionResult AssignProject(int id)
        {
            if (User.IsInRole("MD"))
            {

                ApplicationDbContext _context = new ApplicationDbContext();
                PostContent item = new PostContent();
                item = _context.Posts.Find(id);
                item.assign = true;
                item.MDID = Session["id"].ToString();
                _context.SaveChanges();
                return RedirectToAction("Index", "MD");
            }
            return HttpNotFound();


        }

        //shows his Qualifications and experience.
        public ActionResult StatisticalDiagrams()
        {

            if (User.IsInRole("MD"))
            {
                return View();
            }
            return HttpNotFound();

        }

        public ActionResult ControlProjects()
        {
            if (User.IsInRole("MD"))
            {
                MDPost MD = new MDPost(Session["id"].ToString(), false);
                return View(MD);
            }
            return HttpNotFound();
        }

        public ActionResult AssignProjectAsDelivered(int id)
        {
            if (User.IsInRole("MD"))
            {

                PostContent x = new PostContent();
                x = _context.Posts.Find(id);
                x.delivered = true;
                _context.SaveChanges();
                return RedirectToAction("ControlProjects", "MD");
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult SetSchedule(int id)
        {
            var currentSchedule = _context.Posts.Find(id);
            PostContent post;
            if (currentSchedule.Price == null)
            {
                post = new PostContent { Id = id };
            }
            else
            {
                post = currentSchedule;
            }

            return View(post);

        }

        [HttpPost]
        public ActionResult SetSchedule(PostContent ScheduleData)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            if (ScheduleData.EndingPoint == null || ScheduleData.StartingPoint == null || ScheduleData.Price == null)
            {
                PostContent post = new PostContent { Id = ScheduleData.Id };
                return View("SetSchedule", post);
            }
            else
            {

                PostContent x = _context.Posts.Find(ScheduleData.Id);
                x.Price = ScheduleData.Price;
                x.StartingPoint = ScheduleData.StartingPoint;
                x.EndingPoint = ScheduleData.EndingPoint;
                _context.SaveChanges();
                return RedirectToAction("ControlProjects");

            }

        }
        public ActionResult RemoveMT(int id) // project id
        {
            if (User.IsInRole("MD"))
            {
                AllTeams teams = new AllTeams();
                teams.Team = teams._context.Team.Where(x => x.ProjectID == id).ToList();
                return View(teams);
            }
            return HttpNotFound();
        }

        public ActionResult RemoveMTL(int id)
        {
            if (User.IsInRole("MD"))
            {
                AllTeams teams = new AllTeams();
                teams.Team = teams._context.Team.Where(x => x.ProjectID == id).ToList();
                return View(teams);
            }
            return HttpNotFound();
        }

        public ActionResult DeleteMTL(string id)
        {
            if (User.IsInRole("MD"))
            {
                int projectID = Convert.ToInt32(Session["SchedulPro"]);
                List<Team> X = new List<Team>();
                X = _context.Team.Where(c => c.ProjectID == projectID).Where(m => m.MTLID == id).ToList();
                foreach (var Team in X)
                {
                    if (Team.MTLID == id)
                    {

                        Team.MTLID = null;
                    }

                }

                _context.SaveChanges();
                return RedirectToAction("ControlProjects", "MD");
            }
            return HttpNotFound();

        }

        public ActionResult DeleteMT(string id)
        {
            if (User.IsInRole("MD"))
            {

                int projectID = Convert.ToInt32(Session["SchedulPro"]);
                List<Team> X = new List<Team>();
                X = _context.Team.Where(c => c.ProjectID == projectID).Where(m => m.MTID == id).ToList();
                foreach (var Team in X)
                {
                    if (Team.MTID == id)
                    {

                        Team.MTID = null;
                    }

                }

                _context.SaveChanges();
                return RedirectToAction("ControlProjects", "MD");
            }
            return HttpNotFound();
        }

        public ActionResult LeaveProject(int id)
        {
            if (User.IsInRole("MD"))
            {
                PostContent x = new PostContent();
                x = _context.Posts.Find(id);
                x.MDID = null;
                AllTeams MDTeams = new AllTeams();
                //if the user is MD Delte ALL his Teams
                foreach (var item in MDTeams.Team)
                {
                    if (item.ProjectID == id)
                    {
                        Team xteam = _context.Team.Find(item.id);
                        var T = _context.Team.Remove(xteam);
                        _context.SaveChanges();
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("ControlProjects", "MD");
            }
            return HttpNotFound();
        }


        public ActionResult ListMT(int id)//id of project
        {

            if (User.IsInRole("MD"))
            {
                Session["SchedulPro"] = id;

                //List<ApplicationUser> Customer = new List<ApplicationUser>();

                var list = _context.Users.ToList();
                List<ApplicationUser> MT = new List<ApplicationUser>();
                var teams = _context.Team.Where(m => m.ProjectID == id).ToList();

                //MTLorMTPost MTL = new MTLorMTPost((string)(Session["id"]), false);

                foreach (var team in teams)
                {
                    foreach (var user in list)
                    {
                        //var isInRole = UserManager.IsInRole(user.Id, "MT");
                        if (user.Id == team.MTID)
                        {
                            if (!MT.Contains(user))
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

        public ActionResult ListMTL(int id)//id of project
        {

            if (User.IsInRole("MD"))
            {
                Session["SchedulPro"] = id;

                //List<ApplicationUser> Customer = new List<ApplicationUser>();

                var list = _context.Users.ToList();
                List<ApplicationUser> MTL = new List<ApplicationUser>();
                var teams = _context.Team.Where(m => m.ProjectID == id).ToList();

                //MTLorMTPost MTL = new MTLorMTPost((string)(Session["id"]), false);

                foreach (var team in teams)
                {
                    foreach (var user in list)
                    {
                        //var isInRole = UserManager.IsInRole(user.Id, "MT");
                        if (user.Id == team.MTLID)
                        {
                            if (!MTL.Contains(user))
                                MTL.Add(user);
                        }
                    }

                }
                return View(MTL);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult AppendingProjects()
        {
            if (User.IsInRole("MD"))
            {
                AllPosts Posts = new AllPosts( false);
                return View(Posts);
            }
            return HttpNotFound();
        }
        public ActionResult ApprovePrpject(int id)
        {
            if (User.IsInRole("MD"))
            {
                PostContent x = _context.Posts.Find(id);
                x.Approvement = true;
                _context.SaveChanges();
                return RedirectToAction("AppendingProjects", "MD");
            }
            return HttpNotFound();
        }

        //show all mlt in the system
        
        public ActionResult AssignMTLs(int id, string searching)//this id for ID project(int)
        {

            if (User.IsInRole("MD"))
            {

                
                UsersNoti MTLNoti = new UsersNoti();
                
                var list = _context.Users.ToList();


                List<ApplicationUser> MTL = new List<ApplicationUser>();
                List<ApplicationUser> Final = new List<ApplicationUser>();

                foreach (var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MTL");
                    if (isInRole == true)
                    {
                        List<Notification> MTLNotification = new List<Notification>();
                        MTLNotification = _context.Notification.Where(d => d.ToID == user.Id).Where(m=>m.postID == id).ToList();
                        
                        foreach(var item in MTLNotification)
                        {
                            if (item.stutes != false)
                            {
                                Final.Add(user);

                            }
          
                        }
                        

                    }
                }
                foreach(var user in list)
                {
                    var isInRole = UserManager.IsInRole(user.Id, "MTL");
                    if (isInRole == true)
                    {

                        if (!Final.Contains(user))
                            MTL.Add(user);

                    }
                }
  
                if (!String.IsNullOrEmpty(searching))
                {
                    var MTLSearching = MTL.Where(d => d.Email.Contains(searching)).ToList();
                    MTLNoti.Users = MTLSearching;
                    return View(MTLNoti);
                }
                //var idrr = id;
                //MTLNoti.Notification.postID = id;
                Session["Pro"] = id;
                MTLNoti.Users = MTL;
                return View(MTLNoti);

            }
            return HttpNotFound();

        }
        [HttpPost]
        public ActionResult AssignMTLs(UsersNoti MTLNoti, string ToID)
        {
            MTLNoti.Notification.FromID = User.Identity.GetUserId();
            MTLNoti.Notification.postID = Convert.ToInt32(Session["Pro"]);
           
            PostContent test = _context.Posts.Find(MTLNoti.Notification.postID);
           
            MTLNoti.Notification.message = "Requested You To Join Project: ";
            MTLNoti.Notification.createdOn = DateTime.Now;
            MTLNoti.Notification.ToID = ToID;
            
            var x = _context.Notification.Add(MTLNoti.Notification);
            _context.SaveChanges();

            return RedirectToAction("AssignMTLs", "MD");
            
        }




        //get All MTs 
        public ActionResult AssignMTs(int id, string searching)//this id for ID project(int)
        {

            if (User.IsInRole("MD"))
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
            if (User.IsInRole("MD"))
            {
                MTLNoti.Notification.FromID = User.Identity.GetUserId();
                MTLNoti.Notification.postID = Convert.ToInt32(Session["Pro"]);

                PostContent test = _context.Posts.Find(MTLNoti.Notification.postID);

                MTLNoti.Notification.message = "Requested You To Join Project: ";
                MTLNoti.Notification.createdOn = DateTime.Now;
                MTLNoti.Notification.ToID = ToID;

                var x = _context.Notification.Add(MTLNoti.Notification);
                _context.SaveChanges();

                return RedirectToAction("AssignMTs", "MD");
            }
            return HttpNotFound();

        }

        public ActionResult MakeDelivered(int id)
        {
            if (User.IsInRole("MD"))
            {
                var post = _context.Posts.Find(id);
                post.delivered = true;
                _context.SaveChanges();

                return RedirectToAction("NotDelivered", "MD");
            }
            return HttpNotFound();
        }
        
        public ActionResult LeaveProjectRequests()
        {
            if (User.IsInRole("MD"))
            {
                List<Notification> notificationList = new List<Notification>();
                var session = (string)(Session["id"]);
                var notification = _context.Notification.Where(d => d.ToID == session).Where(m=>m.Type == 1).ToList();
                return View(notification);

            }
            return HttpNotFound();
        }
        //Null : Means that There is No Leave Requests
        // 1 : Means that There is a Leave Request
        // 0 : MEans that the MD Has been canceled the request
        // 3 : MEans that the MD Has been Accepts the request
        [HttpPost]
        public ActionResult RequestLeaveProjectAction(int postID, string MTFrom, int accept = 0, int refuse = 0 )
        {
            if (User.IsInRole("MD"))
            {
                List<Notification> All = new List<Notification>();
                All = _context.Notification.Where(m => m.postID == postID).Where(m=>m.Type == 1).ToList();
                if (accept == 1)
                {
                    


                    foreach (var item in All)
                    {
                        if (item.ToID == (string)(Session["id"]))
                        {
                            item.Type = 3;//Mean that the MD Accept the Requests
                        }
                    }

                    _context.SaveChanges();
                    var MTTeam = _context.Team.Where(m => m.MTID == MTFrom).Where(m => m.ProjectID == postID).ToList();
                    foreach(var item in MTTeam)
                    {
                        var x = _context.Team.Remove(item);
                        
                    }
                    _context.SaveChanges();

                   
                }
                if (refuse == 2)
                {

                    foreach (var item in All)
                    {
                        if (item.ToID == (string)(Session["id"]))
                        {
                            item.Type = 0;//This mean that MD Refused 
                        }

                    }
                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "MD");
                //return View();
            }
            return HttpNotFound();
        }


    }
}