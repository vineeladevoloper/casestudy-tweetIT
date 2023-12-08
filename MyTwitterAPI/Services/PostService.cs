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
                Post postWithUser = context.Posts.Where(p => p.PostId == postId)
                .Include(p => p.User).SingleOrDefault();
                if (postWithUser == null)
                {
                    return null;
                }
                PostDTO newpost = _mapper.Map<PostDTO>(postWithUser);
                var name = context.Users.SingleOrDefault(u => u.UserId == newpost.UserId);
                newpost.User = name.UserName;
                newpost.UserType = name.UserType;

   
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

                List<Post> activeMatchingPosts = context.Posts
                    .Include(p => p.User)
                    .Where(p => p.UserId == userId &&
                                EF.Functions.Like(p.Title, $"%{searchTerm}%") &&
                                p.Active == 1)
                    .ToList();

                List<PostDTO> matchingPostDTOs = _mapper.Map<List<PostDTO>>(activeMatchingPosts);

                foreach (var postDTO in matchingPostDTOs)
                {
                    Console.WriteLine(postDTO.PostId);
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.User = name.UserName;
                    postDTO.UserType = name.UserType;
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
                List<Post> activeMatchingPosts = context.Posts.Include(p => p.User)
                    .Where(p => p.Active == 1 &&
                                EF.Functions.Like(p.Title, $"%{searchTerm}%") &&
                                p.User.UserType != "Blocked")
                    .ToList();

                List<PostDTO> matchingPostDTOs = _mapper.Map<List<PostDTO>>(activeMatchingPosts);
                foreach (var postDTO in matchingPostDTOs)
                {
                    Console.WriteLine(postDTO.PostId);
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.User = name.UserName;
                    postDTO.UserType= name.UserType;
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
                List<Post> activeUserPosts = context.Posts
                    .Include(p => p.User)
                    .Where(p => p.UserId == userId && p.Active == 1) // Filter out only active posts for the specific user
                    .ToList();

                List<PostDTO> userPostDTOs = _mapper.Map<List<PostDTO>>(activeUserPosts);

                foreach (var postDTO in userPostDTOs)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.User = name.UserName;
                    postDTO.UserType = name.UserType;
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
                List<Post> activePostsWithUserDetails = context.Posts.Include(p => p.User)
            .Where(p => p.Active == 1) // Filter out only active posts
            .ToList();

                List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(activePostsWithUserDetails
                    .Where(p => p.User.UserType != "Blocked")); // Replace "Blocked" with the actual value for a blocked user

                foreach (var postDTO in postDTOs)
                {
                    var name = context.Users.SingleOrDefault(u => u.UserId == postDTO.UserId);
                    postDTO.User = name.UserName;
                    postDTO.UserType= name.UserType;
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
                    post.Active = 0; // Marking the post as inactive
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
