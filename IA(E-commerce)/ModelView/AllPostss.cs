using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;

namespace IA_E_commerce_.ModelView
{
    public class AllPosts
    {
        public List<Image> img { get; set; }
        public List<PostContent> pst { get; set; }
        public ApplicationDbContext _context { get; set; }
        public AllPosts()
        {

            _context = new ApplicationDbContext();
            img = new List<Image>();
           
            img = (from d in _context.Images select d).ToList();
            
            pst = new List<PostContent>();
            pst = (from d in _context.Posts select d).ToList();
        }
    }
}