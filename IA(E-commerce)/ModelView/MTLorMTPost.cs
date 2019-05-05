using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
using IA_E_commerce_.ModelView;
namespace IA_E_commerce_.ModelView
{
    public class MTLorMTPost
    {
        public string MTLID { get; set; }
        public string MTID { get; set; }

        public List<PostContent> MTLPosts { get; set; }
        public List<PostContent> MTPosts { get; set; }

        public List<Team> MTLTeams { get; set; }
        public List<Team> MTTeams { get; set; }
        public ApplicationDbContext _context { get; set; }


        public MTLorMTPost(string id, bool Delivered, String Choice)
        {

            AllPosts Posts = new AllPosts();
            AllTeams Teams = new AllTeams();

            _context = new ApplicationDbContext();

            //if iwant to get all post of MTL
            if (Choice == "MTL")
            {
                this.MTLID = id;
                MTLPosts = new List<PostContent>();
                MTLTeams = new List<Team>();

                //Get All Teams That the MTL Join To IT
                foreach (var Team in Teams.Team)
                {
                    if (Team.MTLID == this.MTLID)
                    {
                        MTLTeams.Add(Team);
                    }

                }

                //Get All Project That The MTL Particibate in
                foreach (var Team in MTLTeams)
                {
                    foreach (var Post in Posts.pst)
                    {
                        if (Team.ProjectID == Post.Id && Post.delivered == Delivered)
                        {
                            MTLPosts.Add(Post);
                        }
                    }
                }

            }

            //if iwant to get all post of MTL
            if (Choice == "MT")
            {
                this.MTID = id;
                MTPosts = new List<PostContent>();
                MTTeams = new List<Team>();

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
                        if (Team.ProjectID == Post.Id && Post.delivered == Delivered)
                        {
                            MTPosts.Add(Post);
                        }
                    }
                }

            }


        }
    }
}



