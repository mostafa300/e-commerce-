using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Reflection.Emit;

namespace IA_E_commerce_.Models
{
    public class Team
    {
        
        public int id { get; set; }
        ApplicationUser MTL { get; set; }
        public string MTLID { get; set; }
        ApplicationUser MT { get; set; }
        public string MTID { get; set; }
        PostContent Project { get; set; }
        public int ProjectID { get; set; }
        public bool MTRequest { get; set; }
        public bool MTLRequest { get; set; }

        
    }
  

}
