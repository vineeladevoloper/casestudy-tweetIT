using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface ICommentService
    {
        void AddComment(Comment Comment);
        List<CommentDTO> GetAllCommentsForPost(int postId);
    }
}
