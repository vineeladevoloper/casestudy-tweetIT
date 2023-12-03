using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTwitterAPI.Entities
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
