using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IA_E_commerce_.Models
{
    public class Image
    {
        public int Id { get; set; }
        public PostContent Post { get; set; }
        public int PostID { get; set; }
        [Required]
        public byte[] ImageValue { get; set; }
        

        public static List<Image> GetAllImages()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            List<Image> AllImages = _context.Images.ToList();
            return AllImages;
        }
    }
}