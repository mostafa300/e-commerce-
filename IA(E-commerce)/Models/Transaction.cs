using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.Models
{
    public class Transaction
    {
        public string id { get; set; }
        ApplicationUser IdentUser { get; set; }
        public string MDid { get; set; }
        public string MTLid { get; set; }
        public string MTid { get; set; }
        PostContent post { get; set; }
        public int Post_id { get; set; } 
    }
}