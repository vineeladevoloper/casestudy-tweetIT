using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Services;
using System.Data;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public PostController(IPostService postService, IMapper mapper, ILog logger)
        {
            this.postService = postService;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpPost, Route("AddPost")]
        [Authorize(Roles = "User")]
        public IActionResult AddPost(PostWithoutIdDTO postdto)
        {
            Post post=_mapper.Map<Post>(postdto);
            post.DateandTime = DateTime.Now;
            post.ValidatedorBlocked = null;
            post.ActionDoneById = null;
            post.ActionDOneUser = null;

            postService.AddPost(post);
            return StatusCode(200, post);
        }
        [HttpGet,Route("GetAllPosts")]
        [AllowAnonymous]
        public IActionResult GetAllPost() 
        {
            try
            {
                List<Post> posts = postService.GetAllPost();
                List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(posts);
                return StatusCode(200, postDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet, Route("GetPostById/{postId}")]
        [AllowAnonymous]
        public IActionResult GetPostById(int postId)
        {
            try
            {
                Post post = postService.GetPostById(postId);
                return StatusCode(200, post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut,Route("EditPost")]
        [Authorize(Roles = "User")]
        public IActionResult EditPost(PostDTO postdto)
        {
            try
            {
                var result = postService.EditPost(postdto);
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
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpDelete,Route("DeletePost/{PostId}")]
        [AllowAnonymous]
        public IActionResult DeletePost(int PostId) 
        {
            try
            {
                var result = postService.DeletePost(PostId);
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
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet, Route("GetPostByYear/{year}")]
        [AllowAnonymous]
        public IActionResult GetPostByYear(int year)
        {
            try
            {
                List<Post> posts = postService.GetPostByYear(year);
                List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(posts);
                return StatusCode(200, postDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

    }
}
