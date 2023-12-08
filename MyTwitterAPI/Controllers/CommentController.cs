using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Services;
using System.Xml.Linq;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public CommentController(ICommentService commentService, IMapper mapper, ILog logger)
        {
            this.commentService = commentService;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpPost, Route("AddComment")]
        //[Authorize(Roles = "User")]
        public IActionResult AddComment(CommentWithoutIdDTO commentdto)
        {
            Comment comment = _mapper.Map<Comment>(commentdto);
            comment.DateandTime = DateTime.Now;
            comment.ValidatedOrBlocked = null;
            comment.ActionDoneById = null;
            comment.ActionDoneuser = null;

            commentService.AddComment(comment);
            return StatusCode(200, comment);
        }
        [HttpGet, Route("GetAllComment")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAllComment()
        {
            try
            {
                List<CommentDTO> comments = commentService.GetAllComment();
               // List<CommentDTO> commentDTOs = _mapper.Map<List<CommentDTO>>(comments);
                return StatusCode(200, comments);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpGet, Route("GetCommentById/{commentId}")]
        //[AllowAnonymous]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                CommentDTO comment = commentService.GetCommentById(commentId);
                return StatusCode(200, comment);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet, Route("GetAllCommentsForPost/{postId}")]
        public IActionResult GetAllCommentsForPost(int postId)
        {
            try
            {
                List<CommentDTO> commentsForPost = commentService.GetAllCommentsForPost(postId);
                return Ok(commentsForPost);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (e.g., log it)
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut, Route("EditComment")]
        //[Authorize(Roles = "User")]
        public IActionResult EditComment(CommentDTO commentdto)
        {
            try
            {
                var result = commentService.EditComment(commentdto);
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
        [HttpDelete, Route("DeleteComment/{CommentId}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteComment(int CommentId)
        {
            try
            {
                var result = commentService.DeleteComment(CommentId);
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
    }
}
