using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Services;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public LikeController(ILikeService likeService, IMapper mapper, ILog logger)
        {
            this.likeService = likeService;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpPost, Route("AddLike")]
       [Authorize(Roles = "User")]
       //
        public IActionResult AddLike(LikeWithoutIdDTO likedto)
        {
            Like like = _mapper.Map<Like>(likedto);
            like.DateTime = DateTime.Now;


            likeService.AddLike(like);
            return StatusCode(200, like);
        }

        [HttpGet, Route("GetLikeByPostAndUser/{postId}/{userId}")]
        [AllowAnonymous]
        //
        public IActionResult GetLikeByPostAndUser(int postId, string userId)
        {
            try
            {
               
                var result = likeService.GetLikeByPostAndUser(postId, userId);
                if (result.Success)
                {
                    return StatusCode(200, postId);
                }
                else
                {
                    return StatusCode(200, 0);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }

        }

        [HttpGet, Route("GetLikesByPost/{postId}")]
        [AllowAnonymous]
        //
        public IActionResult GetLikesByPost(int postId)
        {
            try
            {
                List<LikeDTO> like = likeService.GetLikesByPost(postId);
                return StatusCode(200, like);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteLikeByUserPost/{postId}/{userId}")]
        [AllowAnonymous]
        //
        public IActionResult DeleteLikeByUserPost(int postId, string userId)
        {
            try
            {
                var result = likeService.DeleteLikeByUserPost(postId,userId);
                if (result.Success)
                {
                    return StatusCode(200, result.Message);
                }
                else
                {
                    return StatusCode(200, result.Message);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }
    }
}
