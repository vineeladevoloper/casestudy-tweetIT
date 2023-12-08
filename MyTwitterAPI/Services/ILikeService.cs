using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface ILikeService
    {
        LikeDTO AddLike(Like like);
        List<Like> GetAllLike();
        Like GetLikeById(int LikeId);
        ResultModel GetLikeByPostAndUser(int postId, string userId);
        ResultModel DeleteLikeByUserPost(int postId, string userId);
        List<LikeDTO> GetLikesByPost(int postId);
        ResultModel DeleteLike(int LikeId);
    }
}
