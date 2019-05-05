using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IA_E_commerce_.Models
{
    public class FeedbackEvaluate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Rate { get; set; }
        public ApplicationUser MD { get; set; }
        public string MDID { get; set; }
        public ApplicationUser MT { get; set; }
        public string MTID { get; set; }
        public ApplicationUser MTL { get; set; }
        public string MTLID { get; set; }
        public PostContent FeedProject { get; set; }
        public int FeedProjectID { get; set; }
    }
}