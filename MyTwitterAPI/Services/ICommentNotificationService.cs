using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface ICommentNotificationService
    {
        void AddCommentNotification(CommentNotification notify);
        List<NotificationDTO> GetAllCommentNotifications();
        List<NotificationDTO> GetCommentNotificationsByUser(string userId);
        void MarkAsRead(int commentNotificationId);
    }
}
