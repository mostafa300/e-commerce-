using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;

namespace IA_E_commerce_.ModelView
{
    public class MDPost

    {
        public string MDid;

        public List<Image> img { get; set; }
        public List<PostContent> pst { get; set; }
        public ApplicationDbContext _context { get; set; }
        public MDPost(string id, bool delivered)
        {
            this.MDid = id;
            _context = new ApplicationDbContext();
            img = new List<Image>();

            img = (from d in _context.Images select d).ToList();

            pst = new List<PostContent>();
            pst = (from d in _context.Posts select d).Where(c => c.MDID == this.MDid).Where(m => m.delivered == delivered).ToList();




        }
    }
}