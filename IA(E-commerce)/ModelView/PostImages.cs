using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.ModelView
{
    public class PostImages
    {
        public List<Image> img { get; set; }

        public PostContent pst { get; set; }

        public PostImages()
        {
            img = new List<Image>();
            pst = new PostContent();
        }
        
    }
}