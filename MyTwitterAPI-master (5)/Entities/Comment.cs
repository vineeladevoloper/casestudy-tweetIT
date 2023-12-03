using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace MyTwitterAPI.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int  CommentId { get; set; }
        [Required]
        public string? CommentText { get; set;}
        [Required]
        public string? UserId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public DateTime DateandTime { get; set; }
        public string? ValidatedOrBlocked { get; set; }
        public string? ActionDoneById { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("ActionDoneById")]
        public User ActionDoneuser { get; set; }
    }
}
