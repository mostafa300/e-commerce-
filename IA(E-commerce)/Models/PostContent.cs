using IA_E_commerce_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace IA_E_commerce_.Models
{
    public class PostContent
    {
        // user id foreign key
        [Required]
        public string Title { get; set; }

        public int Id { get; set; }

        [Required]

        public string Description { get; set; }
        public ApplicationUser UserPost { get; set;}
        public string Relationid { get; set; }
        public bool delivered { get; set; }
        public bool assign { get; set; }
        public string MDID { get; set; }
        public bool MDRequest { get; set; }
        public bool Approvement { get; set; }
        [DisplayName("Start Date")]
        //[Required(ErrorMessage = "Desired Format is yyyy-mm-dd")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd}")]
        public DateTime? StartingPoint { get; set; }

        [DisplayName("End Date")]
        //[Required(ErrorMessage = "Desired Format is yyyy-mm-dd")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd}")]
        public DateTime? EndingPoint { get; set; }

        //[Required]
        public float? Price { get; set; }
        public static List<PostContent> GetAllPost()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            List<PostContent> AllPosts = _context.Posts.ToList();
            return AllPosts;
        }

        public static implicit operator List<object>(PostContent v)
        {
            throw new NotImplementedException();
        }
    }

}