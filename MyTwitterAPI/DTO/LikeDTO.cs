using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.DTO
{
    public class LikeDTO
    {
        public int LikeId { get; set; }
        public int PostId { get; set; }
        public string? UserId { get; set; }
        public DateTime DateTime { get; set; }  
        public string User { get; set; }
    }
}
