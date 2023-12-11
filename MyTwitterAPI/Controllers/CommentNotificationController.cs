using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Services;
using System.Data;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentNotificationController : ControllerBase
    {
        private readonly ICommentNotificationService commentNotificationService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILog _logger;

        public CommentNotificationController(ICommentNotificationService commentNotificationService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            this.commentNotificationService = commentNotificationService;
            this._mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpPost, Route("AddCommentNotification")]
        [Authorize(Roles = "User")]
        //
        public IActionResult AddCommentNotification(NotificationWithOutIDDTO notify)
        {
            try
            {
                CommentNotification notifydto = _mapper.Map<CommentNotification>(notify);
                notifydto.NotificationTime = DateTime.Now;
                commentNotificationService.AddCommentNotification(notifydto);
                return StatusCode(200, notify);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetCommentNotificationsByUser/{userId}")]
        [Authorize(Roles = "User")]
        //
        public IActionResult GetCommentNotificationsByUser(string userId)
        {
            try
            {
                List<NotificationDTO> notifications = commentNotificationService.GetCommentNotificationsByUser(userId);
                return StatusCode(200, notifications);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut, Route("MarkAsRead/{commentNotificationId}")]
        [Authorize(Roles = "User")]
        //
        public IActionResult MarkAsRead(int commentNotificationId)
        {
            try
            {
                commentNotificationService.MarkAsRead(commentNotificationId);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
