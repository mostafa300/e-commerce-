using IA_E_commerce_.Models;
using System;
using System.Collections.Generic;
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