using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface ICommentService
    {
        void AddComment(Comment Comment);
        List<CommentDTO> GetAllComment();
        CommentDTO GetCommentById(int CommentId);
        List<CommentDTO> GetAllCommentsForPost(int postId);
        ResultModel EditComment(CommentDTO commentdto);
        ResultModel DeleteComment(int CommentId);
    }
}
