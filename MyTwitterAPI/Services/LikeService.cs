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

        public LikeDTO AddLike(Like like)
        {
            try
            {
                context.Likes.Add(like);
                context.SaveChanges();

                // Retrieve the added Like entity with any additional details
                Like addedLike = context.Likes.Single(l => l.LikeId == like.LikeId);

                // Map the added Like entity to a LikeDTO
                LikeDTO likeDTO = _mapper.Map<LikeDTO>(addedLike);

                // Optionally, fetch additional details for the user
                var user = context.Users.SingleOrDefault(u => u.UserId == likeDTO.UserId);
                likeDTO.User = user?.Name;

                return likeDTO;
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

        public ResultModel DeleteLikeByUserPost(int postId, string userId)
        {
            try
            {
                Like like = context.Likes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId);

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

        public ResultModel GetLikeByPostAndUser(int postId, string userId)
        {
            try
            {
                Console.WriteLine(postId);
                Console.WriteLine(userId);
                Like like= context.Likes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId);
                if(like != null)
                {
                    LikeDTO likedto = _mapper.Map<LikeDTO>(like);
                    var name = context.Users.SingleOrDefault(u => u.UserId == likedto.UserId);
                    likedto.User = name.Name;
                    return new ResultModel { Success = true, Message = "found." };
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

        public List<LikeDTO> GetLikesByPost(int postId)
        {
            try
            {

                List<Like> likes = context.Likes.Where(l => l.PostId == postId).ToList();
                List<LikeDTO> likeDTOs = _mapper.Map<List<LikeDTO>>(likes);

                foreach (var likeDTO in likeDTOs)
                {
                    var user = context.Users.SingleOrDefault(u => u.UserId == likeDTO.UserId);
                    likeDTO.User = user?.Name;
                }
                return likeDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
