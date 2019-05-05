using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IA_E_commerce_.Models
{
    public class Notification
    {
        public int id { get; set; }
        public ApplicationUser From { get; set; }
        public string FromID { get; set; }
        public ApplicationUser To { get; set; }
        public string ToID { get; set; }
        public string message { get; set; }
        public bool? stutes { get; set; }
        public DateTime createdOn { get; set; }
        public PostContent post { get; set; }
        public int postID { get; set; }
        public int Type { get; set; }
    }
}