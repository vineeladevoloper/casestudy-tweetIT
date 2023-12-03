using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public class PostService : IPostService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public PostService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }
        public void AddPost(Post post)
        {
            try
            {
                context.Posts.Add(post);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Post> GetAllPost()
        {
            try
            {
                return context.Posts.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResultModel DeletePost(int postId)
        {
            try
            {
                Post post = context.Posts.SingleOrDefault(p => p.PostId == postId);

                if (post != null)
                {
                    context.Posts.Remove(post);
                    context.SaveChanges();

                    return new ResultModel { Success = true, Message = "Post deleted successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Post not found." };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }
        public ResultModel EditPost(PostDTO postdto)
        {
            try
            {
                Post newpost = _mapper.Map<Post>(postdto);
                Post existingpost = context.Posts.SingleOrDefault(p => p.PostId == postdto.PostId);
                if (existingpost != null)
                {
                    context.Entry(existingpost).State = EntityState.Detached;
                    newpost.DateandTime=existingpost.DateandTime;
                    newpost.ValidatedorBlocked=existingpost.ValidatedorBlocked;
                    newpost.ActionDoneById=existingpost.ActionDoneById;
                    newpost.User=existingpost.User;
                    newpost.ActionDOneUser=existingpost.ActionDOneUser;
                    context.Posts.Update(newpost);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "Post edited successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Post not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }
        public Post GetPostById(int postId)
        {
            try
            {
                return context.Posts.SingleOrDefault(p => p.PostId == postId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Post> GetPostByYear(int year)
        {
            try
            {
                List<Post> posts = context.Posts.Where(p => p.DateandTime.Year == year).ToList();
                return posts;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
