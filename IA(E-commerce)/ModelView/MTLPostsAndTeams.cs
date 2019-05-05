using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
using IA_E_commerce_.ModelView;
namespace IA_E_commerce_.ModelView
{
    public class MTLPostsAndTeams
    {
        public string MTLID { get; set; }
        public List<PostContent> MTLPosts { get; set; }
        public List<Image> MTLImages { get; set; }
        public List<Team> MTLTeams { get; set; }
        public ApplicationDbContext _context { get; set; }

        public MTLPostsAndTeams() { 
}
        public MTLPostsAndTeams(string id , bool Actor)
        {
            this.MTLID = id;
            AllPosts Posts = new AllPosts();
            AllTeams Teams = new AllTeams();
            // AllImages Images = new AllImages();
            MTLPosts = new List<PostContent>();
            MTLTeams = new List<Team>();
            MTLImages = new List<Image>();
            _context = new ApplicationDbContext();
            //Get All Images
            // MTLImages = (from d in _context.Images select d).ToList();
            //Get All Teams That the MTL Join To IT
            if(Actor == true)
            {
                foreach (var Team in Teams.Team)
                {
                    if (Team.MTLID == this.MTLID)
                    {

                        MTLTeams.Add(Team);
                    }

                }
            }
            if (Actor == false)
            {
                foreach (var Team in Teams.Team)
                {
                    if (Team.MTID == this.MTLID)
                    {

                        MTLTeams.Add(Team);
                    }

                }
            }
            

            //Get All Project That The MTL Particibate in
            foreach (var Team in MTLTeams)
            {
                foreach (var Post in Posts.pst)
                {
                    if (Team.ProjectID == Post.Id)
                    {
                        MTLPosts.Add(Post);
                    }
                }
            }

        }


        
    }
}