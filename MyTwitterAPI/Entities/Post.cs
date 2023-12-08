using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.Entities
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public int Active { get; set; } = 1;
        public DateTime DateandTime { get; set; }
        public string? ValidatedorBlocked { get; set; }
        public string? ActionDoneById { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("ActionDoneById")]
        public User? ActionDOneUser { get; set; }
    }
}
