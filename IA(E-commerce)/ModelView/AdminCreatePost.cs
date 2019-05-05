using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.ModelView
{
    public class AdminCreatePost
    {
        public List<Image> img { get; set; }

        public PostContent pst { get; set; }
        public List<ApplicationUser> Customer { get; set; }

        public AdminCreatePost()
        {
            img = new List<Image>();
            pst = new PostContent();
            Customer = new List<ApplicationUser>();
        }
    }
}