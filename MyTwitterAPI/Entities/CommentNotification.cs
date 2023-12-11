using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTwitterAPI.Entities
{
    [Table("CommentNotifications")]
    public class CommentNotification
    {
        [Key]
        public int CommentNotificationId { get; set; }

        [Required]
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }

        [Required]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime NotificationTime { get; set; }

        public int Read { get; set; } = 0;
    }
}
