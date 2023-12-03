using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface ICommentService
    {
        void AddComment(Comment Comment);
        List<Comment> GetAllComment();
        Comment GetCommentById(int CommentId);
        ResultModel EditComment(CommentDTO commentdto);
        ResultModel DeleteComment(int CommentId);
    }
}
