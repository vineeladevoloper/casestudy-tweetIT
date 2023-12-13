using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;
using MyTwitterAPI.Services;
using System.Data;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly IFollowerService followerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILog _logger;

        public FollowerController(IFollowerService followerService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            this.followerService = followerService;
            this._mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpPost, Route("SendFollowRequest")]
        [Authorize(Roles = "User")]
        //
        public IActionResult SendFollowRequest([FromBody] FollowModel model)
        {
            try
            {
                Console.WriteLine("HI");
                followerService.SendFollowRequest(model.UserId,model.FollowerId);
                _logger.Info("Follow Request successfully");
                return StatusCode(200, "Follow Request successfully");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("AcceptFollowRequest")]
        [Authorize(Roles = "User")]
        //
        public IActionResult AcceptFollowRequest([FromBody] FollowModel model)
        {
            try
            {
                Console.WriteLine("HI");
                followerService.AcceptFollowRequest(model.UserId, model.FollowerId);
                _logger.Info("Follow Request successfully");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("CheckFollowing")]
        [Authorize(Roles = "User")]
        public IActionResult CheckFollowing([FromBody] FollowModel model)
        {
            try
            {
                bool status = followerService.CheckFollowing(model.UserId, model.FollowerId);
                 return StatusCode(200, status);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet,Route("GetPendingFollowRequest/{userId}")]
        [Authorize(Roles = "User")]
        public IActionResult GetPendingFollowRequests(string userId)
        {
            try
            {
                List<User> PendingRequest=followerService.GetPendingFollowRequests(userId);
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(PendingRequest);
                return StatusCode(200, userDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetFollowers/{userId}")]
        [Authorize(Roles = "User")]
        public IActionResult GetFollowers(string userId)
        {
            try
            {
                List<User>followers = followerService.GetFollowers(userId);
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(followers);
                return StatusCode(200, userDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetFollowing/{userId}")]
        [Authorize(Roles = "User")]
        public IActionResult GetFollowing(string userId)
        {
            try
            {
                List<User> followers = followerService.GetFollowing(userId);
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(followers);
                return StatusCode(200, userDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


    }
}
