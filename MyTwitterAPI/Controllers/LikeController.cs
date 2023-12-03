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
        public IActionResult AddLike(LikeWithoutIdDTO likedto)
        {
            Like like = _mapper.Map<Like>(likedto);
            like.DateTime = DateTime.Now;


            likeService.AddLike(like);
            return StatusCode(200, like);
        }
        [HttpGet, Route("GetAlllike")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllLike()
        {
            try
            {
                List<Like> likes = likeService.GetAllLike();
                List<LikeDTO> likeDTOs = _mapper.Map<List<LikeDTO>>(likes);
                return StatusCode(200, likeDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet, Route("GetLikeById/{likeId}")]
        [AllowAnonymous]
        public IActionResult GetLikeById(int likeId)
        {
            try
            {
                Like like = likeService.GetLikeById(likeId);
                return StatusCode(200, like);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteLike/{LikeId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLike(int LikeId)
        {
            try
            {
                var result = likeService.DeleteLike(LikeId);
                if (result.Success)
                {
                    return StatusCode(200, result.Message);
                }
                else
                {
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }
    }
}
