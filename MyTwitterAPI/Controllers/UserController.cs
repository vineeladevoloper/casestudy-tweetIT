using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;
using MyTwitterAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILog _logger;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            this.userService = userService;
            this._mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpGet,Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult GetAllUsers()
        {
            try
            {   
                List<User> users = userService.GetAllUsers();
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);
                return StatusCode(200, userDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);                
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet,Route("GetUserById/{userId}")]
        [AllowAnonymous]
        //
        public IActionResult GetUserById(string userId) 
        {
            try
            {
                User user = userService.GetUserById(userId);
                UserwithPWDDTO userdto = _mapper.Map<UserwithPWDDTO>(user);
                if (user != null)
                {
                    return StatusCode(200, userdto);
                }
                else
                {
                    _logger.Error($"User with Id {userId} not found");
                    return StatusCode(200);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetUsersByName/{searchName}")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult GetUsersByName(string searchName)
        {
            try
            {
                List<User> users = userService.GetUsersByName(searchName);
                List<UserwithPWDDTO> userdtos = _mapper.Map<List<UserwithPWDDTO>>(users);
                if (users != null)
                {
                    return StatusCode(200, userdtos);
                }
                else
                {
                    _logger.Error($"User with name {searchName} not found");
                    return StatusCode(200);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut,Route("UpgradeUserRequest")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult UpgradeUserRequest([FromBody] RequestorReject upgradeRequest)
        {
            try
            {
                string userIdTo = upgradeRequest.UserIdTo;
                string adminUserId = upgradeRequest.AdminUserId;
                var result = userService.UpgradeUserRequest(userIdTo, adminUserId);
                if (result.Success)
                {
                    _logger.Info("User upgraded successfully");
                    return StatusCode(200);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("RejectUserRequest")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult RejectUserRequest([FromBody] RequestorReject upgradeReject)
        {
            try
            {
                string userIdTo = upgradeReject.UserIdTo;
                string adminUserId = upgradeReject.AdminUserId;
                var result = userService.RejectUserRequest(userIdTo, adminUserId);
                if (result.Success)
                {
                    _logger.Info("User Upgrade request rejected");
                    return StatusCode(200);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("UpgradeRequest/{userId}")]
        [Authorize(Roles = "User")]
        //
        public IActionResult UpgradeRequest(string userId)
        {
            try
            {
                userService.UpgradeRequest(userId);
                _logger.Info("Request upgraded successfully");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut, Route("BlockUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult BlockUser([FromBody] RequestorReject blockRequest)
        {
            try
            {
                string userIdTo = blockRequest.UserIdTo;
                string adminUserId = blockRequest.AdminUserId;
                var result = userService.BlockUser(userIdTo, adminUserId);
                if (result.Success)
                {
                    _logger.Info("User upgraded successfully");
                    return StatusCode(200);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost,Route("Register")]
        [AllowAnonymous]
        //
        public IActionResult AddUser(UserwithPWDDTO userdto)
        {
            try
            {
                User user = _mapper.Map<User>(userdto);
                if (userdto.Role == "Admin")
                {
                    user.Type = "Admin";
                    user.Status = "Admin";
                }
                else
                {
                    user.Type = "Normal";
                    user.Status = "Not requested";
                }
                user.ActionById = null;
                user.ActionDoneUser = null;
                var result=userService.AddUser(user);
                if (result.Success)
                {
                    _logger.Info("User added successfully");
                    return StatusCode(200, user);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut,Route("EditUser")]
        [AllowAnonymous]
        //
        public IActionResult EditUser(UserwithPWDDTO userdto) 
        {
            try
            {

                var result = userService.EditUser(userdto);
                if (result.Success)
                {
                    _logger.Info(result.Message);
                    return StatusCode(200, result.Message);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        //
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthReponse authReponse = new AuthReponse();
                if (user != null)
                {
                    authReponse.UserId = user.UserId;
                    authReponse.Role = user.Role;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email,user.UserEmail),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
