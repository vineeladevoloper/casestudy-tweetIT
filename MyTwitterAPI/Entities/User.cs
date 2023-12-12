using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyTwitterAPI.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string? UserId { get; set; }
        [Required]
        [StringLength(25)]
        public string? Name { get; set; }
        [Required]
        [StringLength(20)]
        public string? Role { get; set; }
        [Required]
        [StringLength(50)]
        public string? UserEmail { get; set; }
        [Required]
        [StringLength(15)]
        public string? Password { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? ActionById { get; set; }
        [ForeignKey("ActionById")]
        public User? ActionDoneUser { get; set; }
    }
}
