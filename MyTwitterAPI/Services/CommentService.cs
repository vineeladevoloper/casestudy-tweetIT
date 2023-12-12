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

   }
}
