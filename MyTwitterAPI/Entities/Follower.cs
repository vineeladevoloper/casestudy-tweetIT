using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.Entities
{
    [Table("Followers")]
    public class Follower
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FollowerId { get; set; }
        public int Status{ get; set; }= 0;

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("FollowerId")]
        public User FollowerUser { get; set; }
    }
}
