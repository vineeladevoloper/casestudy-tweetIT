using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;
using System.ComponentModel.Design;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyTwitterAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public CommentService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultModel DeleteComment(int CommentId)
        {
            try
            {
                Comment comment = context.Comments.SingleOrDefault(c => c.CommentId == CommentId);

                if (comment != null)
                {
                    context.Comments.Remove(comment);
                    context.SaveChanges();

                    return new ResultModel { Success = true, Message = "Comment deleted successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Comment not found." };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public ResultModel EditComment(CommentDTO commentdto)
        {
            try
            {
                Comment newcomment = _mapper.Map<Comment>(commentdto);
                Comment existingcomment = context.Comments.SingleOrDefault(c => c.CommentId == commentdto.CommentId);
                if (existingcomment != null)
                {
                    context.Entry(existingcomment).State = EntityState.Detached;
                    newcomment.DateandTime = existingcomment.DateandTime;
                    newcomment.ValidatedOrBlocked= existingcomment.ValidatedOrBlocked;
                    newcomment.ActionDoneById= existingcomment.ActionDoneById;
                    newcomment.User= existingcomment.User;
                    newcomment.Post= existingcomment.Post;
                    newcomment.ActionDoneuser= existingcomment.ActionDoneuser;
                    context.Comments.Update(newcomment);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "Comment edited successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Comment not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public List<CommentDTO> GetAllComment()
        {
            try
            {
                List<Comment> comments = context.Comments.ToList();
                List<CommentDTO> commentDTOs = _mapper.Map<List<CommentDTO>>(comments);

                foreach (var commentDto in commentDTOs)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == commentDto.UserId);
                    var post = context.Posts.SingleOrDefault(p=> p.PostId == commentDto.PostId);
                    commentDto.User = name.Name;
                    commentDto.Post = post.Title;

                }
                return commentDTOs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CommentDTO> GetAllCommentsForPost(int postId)
        {
            try
            {
                List<Comment> commentsForPost = context.Comments
                    .Where(c => c.PostId == postId)
                    .ToList();

                List<CommentDTO> commentDTOsForPost = _mapper.Map<List<CommentDTO>>(commentsForPost);

                foreach (var commentDto in commentDTOsForPost)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == commentDto.UserId);
                    var post = context.Posts.SingleOrDefault(p => p.PostId == commentDto.PostId);
                    commentDto.User = name.Name;
                    commentDto.Post = post.Title;
                }

                return commentDTOsForPost;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public CommentDTO GetCommentById(int CommentId)
        {
            try
            {
                Comment comment= context.Comments.SingleOrDefault(c => c.CommentId == CommentId);
                CommentDTO commentDTO = _mapper.Map<CommentDTO>(comment);
                var name = context.Users.SingleOrDefault(u => u.UserId == comment.UserId);
                var post = context.Posts.SingleOrDefault(p => p.PostId == comment.PostId);
                commentDTO.User = name.Name;
                commentDTO.Post = post.Title;
                return commentDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
