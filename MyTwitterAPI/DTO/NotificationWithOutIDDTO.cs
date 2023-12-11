using MyTwitterAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.DTO
{
    public class NotificationWithOutIDDTO
    {

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public int PostId { get; set; }

        public string CommentText { get; set; }

    }
}
