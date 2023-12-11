namespace MyTwitterAPI.DTO
{
    public class NotificationDTO
    {
        public int CommentNotificationId { get; set; }
        public string SenderId { get; set; }
        public string senderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }

        public string CommentText { get; set; }

        public DateTime NotificationTime { get; set; }

        public int Read { get; set; } = 0;
    }
}
