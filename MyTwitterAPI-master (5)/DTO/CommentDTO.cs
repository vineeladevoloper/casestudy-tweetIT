using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public string? UserId { get; set; }
        public int PostId { get; set; }
    }
}
