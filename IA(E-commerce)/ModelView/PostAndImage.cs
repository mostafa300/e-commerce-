using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.ModelView
{
    public class PostAndImage
    {

        public Image img { get; set; }
        public PostContent pst { get; set; }
        public PostAndImage()
        {
            img = new Image();
            pst = new PostContent();
        }

    }
}
