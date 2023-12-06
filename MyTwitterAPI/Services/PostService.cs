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
        public PostDTO GetPostById(int postId)
        {
            try
            {
                var postWithUser = context.Posts.Where(p => p.PostId == postId)
                .Include(p => p.User).SingleOrDefault();
                if (postWithUser == null)
                {
                    return null;
                }
                PostDTO newpost = _mapper.Map<PostDTO>(postWithUser);
                newpost.UserName = postWithUser.User?.UserName;
                return newpost;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<PostDTO> SearchPostsByTitleAndUserId(string userId, string searchTerm)
        {
            try
            {
                Console.WriteLine(searchTerm);
                Console.WriteLine(userId);
                List<Post> matchingPosts = context.Posts
                    .Include(p => p.User)
                    .Where(p => p.UserId == userId && EF.Functions.Like(p.Title, $"%{searchTerm}%"))
                    .ToList();

                List<PostDTO> matchingPostDTOs = _mapper.Map<List<PostDTO>>(matchingPosts);

                foreach (var postDTO in matchingPostDTOs)
                {
                    Console.WriteLine(postDTO.PostId);
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.UserName = name.UserName;
                }

                return matchingPostDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<PostDTO> SearchPostsByTitle(string searchTerm)
        {
            try
            {
                List<Post> matchingPosts = context.Posts
            .Include(p => p.User)
            .Where(p => EF.Functions.Like(p.Title, $"%{searchTerm}%"))
            .ToList();

                List<PostDTO> matchingPostDTOs = _mapper.Map<List<PostDTO>>(matchingPosts);

                foreach (var postDTO in matchingPostDTOs)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.UserName = name.UserName;
                }

                return matchingPostDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PostDTO> GetPostsByUserId(string userId)
        {
            try
            {
                List<Post> userPosts = context.Posts
                    .Include(p => p.User)
                    .Where(p => p.UserId == userId)
                    .ToList();

                List<PostDTO> userPostDTOs = _mapper.Map<List<PostDTO>>(userPosts);

                foreach (var postDTO in userPostDTOs)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.UserName = name.UserName;
                }

                return userPostDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }
                
        public List<PostDTO> GetAllPost()
        {
            try
            {
                List<Post> postsWithUserDetails = context.Posts.Include(p => p.User).ToList();
                List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(postsWithUserDetails);
                foreach (var postDTO in postDTOs)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.UserName = name.UserName;
                }

                return postDTOs;
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
