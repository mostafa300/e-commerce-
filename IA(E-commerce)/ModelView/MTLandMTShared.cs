using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
using IA_E_commerce_.ModelView;
namespace IA_E_commerce_.ModelView
{
    public class MTLandMTShared
    {
        public string MTID { get; set; }
        public string MTLID { get; set; }
        public List<PostContent> MTPosts { get; set; }
        public List<Image> MTImages { get; set; }
        public List<Team> MTTeams { get; set; }
        public ApplicationDbContext _context { get; set; }
        public FeedbackEvaluate FeedbackForm { get; set; }

        public MTLandMTShared()
        {
        }
        public MTLandMTShared(string id ,string MTLID)
        {
            this.MTID = id;
            this.MTLID = MTLID;
            AllPosts Posts = new AllPosts();
            AllTeams Teams = new AllTeams();
            // AllImages Images = new AllImages();
            MTPosts = new List<PostContent>();
            MTTeams = new List<Team>();
            MTImages = new List<Image>();
            _context = new ApplicationDbContext();
            FeedbackForm = new FeedbackEvaluate();
            //Get All Images
            // MTLImages = (from d in _context.Images select d).ToList();
            //Get All Teams That the MTL Join To IT
             
            
                foreach (var Team in Teams.Team)
                {
                    if (Team.MTID == this.MTID)
                    {

                        MTTeams.Add(Team);
                    }

                }


            //Get All Project That The MTL Particibate in
            foreach (var Team in MTTeams)
            {
                foreach (var Post in Posts.pst)
                {
                    if (Team.ProjectID == Post.Id && Team.MTLID == this.MTLID)
                    {
                        MTPosts.Add(Post);
                    }
                }
            }




        }

    }
}