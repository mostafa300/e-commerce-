using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.ModelView
{

    public class AllImages
    {
        public List<Image> img { get; set; }
        public ApplicationDbContext _context { get; set; }
        public AllImages()
        {
            _context = new ApplicationDbContext();
            img = new List<Image>();

            img = (from d in _context.Images select d).ToList();
        }
    }
}