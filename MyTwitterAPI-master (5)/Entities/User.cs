using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string? UserId { get; set; }
        [Required]
        [StringLength(25)]
        public string? UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string? Role { get; set; }
        [Required]
        [StringLength(50)]
        public string? UserEmail { get; set; }
        [Required]
        [StringLength(15)]
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public string? VerifiedById { get; set; }
        [ForeignKey("VerifiedById")]
        public User? VerifiedUser { get; set; }
    }
}
