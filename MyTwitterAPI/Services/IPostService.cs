using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface IPostService
    {
        void AddPost(Post post);
        List<PostDTO> GetAllPost();
        PostDTO GetPostById(int postId);
        List<PostDTO> SearchPostsByTitle(string searchTerm);
        List<PostDTO> GetPostsByUserId(string userId);
        List<PostDTO> SearchPostsByUser(string searchUser);
        List<PostDTO> SearchPostsByTitleAndUserId(string userId, string searchTerm);
        ResultModel EditPost(PostDTO postdto);
        ResultModel DeletePost(int postId);
        List<Post> GetPostByYear(int year);
    }
}
