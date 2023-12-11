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
        [Authorize(Roles = "User")]
        //
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

        [HttpGet, Route("GetAllCommentsForPost/{postId}")]
        [AllowAnonymous]
        //
        public IActionResult GetAllCommentsForPost(int postId)
        {
            try
            {
                List<CommentDTO> commentsForPost = commentService.GetAllCommentsForPost(postId);
                return Ok(commentsForPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
