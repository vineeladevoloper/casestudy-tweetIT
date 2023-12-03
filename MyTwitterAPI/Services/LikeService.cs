using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;
using System.ComponentModel.Design;

namespace MyTwitterAPI.Services
{
    public class LikeService : ILikeService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public LikeService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        public void AddLike(Like like)
        {
            try
            {
                context.Likes.Add(like);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultModel DeleteLike(int LikeId)
        {
            try
            {
                Like like = context.Likes.SingleOrDefault(l => l.LikeId == LikeId);

                if (like != null)
                {
                    context.Likes.Remove(like);
                    context.SaveChanges();

                    return new ResultModel { Success = true, Message = "Like deleted successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Like not found." };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public List<Like> GetAllLike()
        {
            try
            {
                return context.Likes.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Like GetLikeById(int LikeId)
        {
            try
            {
                return context.Likes.SingleOrDefault(l => l.LikeId == LikeId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
