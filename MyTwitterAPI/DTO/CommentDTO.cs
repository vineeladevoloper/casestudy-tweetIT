using MyTwitterAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTwitterAPI.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public string? UserId { get; set; }
        public int PostId { get; set; }
        public DateTime DateandTime { get; set; }
        public string? User { get; set; }
        public string? Post { get; set; }
    }
}
