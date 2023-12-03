using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;
using System.ComponentModel.Design;
using System.Xml.Linq;

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

        public List<Comment> GetAllComment()
        {
            try
            {
                return context.Comments.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Comment GetCommentById(int CommentId)
        {
            try
            {
                return context.Comments.SingleOrDefault(c => c.CommentId == CommentId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
