using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface IPostService
    {
        void AddPost(Post post);
        List<Post> GetAllPost();
        Post GetPostById(int postId);
        ResultModel EditPost(PostDTO postdto);
        ResultModel DeletePost(int postId);
        List<Post> GetPostByYear(int year);
    }
}
