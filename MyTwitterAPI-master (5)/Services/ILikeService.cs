using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface ILikeService
    {
        void AddLike(Like like);
        List<Like> GetAllLike();
        Like GetLikeById(int LikeId);
        ResultModel DeleteLike(int LikeId);
    }
}
