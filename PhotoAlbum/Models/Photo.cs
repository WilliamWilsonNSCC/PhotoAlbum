using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbum.Models
{
    public class Photo
    {
        // Primary key
        [Display(Name="Id")]
        public int PhotoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string Camera {  get; set; } = string.Empty;

        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

        //Foreign Key
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        //Navigation property
        public Category? Category { get; set; }

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? FormFile { get; set; } //Nullable
    }
}
