namespace PhotoAlbum.Models
{
    public class Comment
    {
        // primary key
        public int CommentId { get; set; }

        public string Body { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        // foreign key
        public int PhotoId { get; set; }

        // navigation property
        public Photo? Photo { get; set; } // nullable
    }
}
