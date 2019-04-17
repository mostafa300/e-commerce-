using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;

namespace IA_E_commerce_.ModelView
{
    public class CustomerPosts
    {
        public string Customerid;
        
        public List<Image> img { get; set; }
        public List<PostContent> pst { get; set; }
        public ApplicationDbContext _context { get; set; }
        public CustomerPosts(string id ,int chocie)
        {
            this.Customerid = id;
            if(chocie == 1)
            {
                _context = new ApplicationDbContext();
                img = new List<Image>();

                img = (from d in _context.Images select d).ToList();

                pst = new List<PostContent>();
                pst = (from d in _context.Posts select d).Where(c => c.Relationid == this.Customerid).ToList();
            }else if(chocie == 2)
            {
                _context = new ApplicationDbContext();
                img = new List<Image>();

                img = (from d in _context.Images select d).ToList();

                pst = new List<PostContent>();
                pst = (from d in _context.Posts select d).Where(c => c.Relationid == this.Customerid).Where(m=>m.delivered == true).ToList();
            }
            else
            {
                _context = new ApplicationDbContext();
                img = new List<Image>();

                img = (from d in _context.Images select d).ToList();

                pst = new List<PostContent>();
                pst = (from d in _context.Posts select d).Where(c => c.Relationid == this.Customerid).Where(m => m.delivered == false).ToList();
            }
            

        }
        
    }
}