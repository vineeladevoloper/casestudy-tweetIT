using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public class UserService : IUserService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public UserService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
 
        }

        public ResultModel AddUser(User user)
        {
            try
            {
                if (context.Users.Any(u => u.UserId == user.UserId))
                {
                    return new ResultModel { Success = false, Message = "User with the same ID already exists." };
                }
                if (context.Users.Any(u => u.UserEmail == user.UserEmail))
                {
                    return new ResultModel { Success = false, Message = "User with the same email already exists." };
                }
                context.Users.Add(user);
                context.SaveChanges();

                return new ResultModel { Success = true, Message = "User added successfully." };
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public ResultModel EditUser(UserwithPWDDTO userdto)
        {
            try
            {
                User newuser=_mapper.Map<User>(userdto);
                User existinguser= context.Users.SingleOrDefault(u => u.UserId == userdto.UserId);
                
                 if (existinguser != null)
                {
                    context.Entry(existinguser).State = EntityState.Detached;
                    newuser.Type = existinguser.Type;
                    newuser.ActionById = existinguser.ActionById;
                    newuser.ActionDoneUser = existinguser.ActionDoneUser;
                    context.Users.Update(newuser);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "User edited successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "User not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return context.Users.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public User GetUserById(string id)
        {

            try
            {
                return context.Users.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<User> GetUsersByName(string searchTerm)
        {
            try
            {
                List<User> users = context.Users
            .Where(u => EF.Functions.Like(u.Name, $"%{searchTerm}%"))
            .ToList();

                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User ValidteUser(string email, string password)
        {
            return context.Users.SingleOrDefault(u => u.UserEmail == email && u.Password == password);
        }

   
        ResultModel IUserService.RejectUserRequest(string userId, string adminId)
        {
            try
            {
                User user = context.Users.SingleOrDefault(u => u.UserId == userId);
                User admin = context.Users.SingleOrDefault(u => u.UserId == adminId && u.Role == "Admin");

                if (user != null || admin != null)
                {
                    context.Entry(user).State = EntityState.Detached;
                    context.Entry(admin).State = EntityState.Detached;
                    user.Status = "Rejected";
                    user.ActionById = admin.UserId;
                    user.ActionDoneUser = admin;
                    context.Users.Update(user);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "User upgraded successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "User or Admin not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }


        ResultModel IUserService.UpgradeUserRequest(string userId, string adminId)
        {
            try
            {
                User user = context.Users.SingleOrDefault(u => u.UserId == userId);
                User admin = context.Users.SingleOrDefault(u => u.UserId == adminId && u.Role == "Admin");

                if (user != null || admin != null)
                {
                    context.Entry(user).State = EntityState.Detached;
                    context.Entry(admin).State = EntityState.Detached;
                    user.Type = "Verified";
                    user.Status = "Upgraded";
                    user.ActionById = admin.UserId;
                    user.ActionDoneUser = admin;
                    context.Users.Update(user);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "User upgraded successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "User or Admin not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }
        ResultModel IUserService.BlockUser(string userId, string adminId)
        {
            try
            {
                User user = context.Users.SingleOrDefault(u => u.UserId == userId);
                User admin = context.Users.SingleOrDefault(u => u.UserId == adminId && u.Role == "Admin");

                if (user != null || admin != null)
                {
                    context.Entry(user).State = EntityState.Detached;
                    context.Entry(admin).State = EntityState.Detached;
                    user.Type = "Blocked";
                    user.ActionById = admin.UserId;
                    user.ActionDoneUser = admin;
                    context.Users.Update(user);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "User Blocked successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "User or Admin not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        void IUserService.UpgradeRequest(string userId)
        {
            try
            {
                User user = context.Users.SingleOrDefault(u => u.UserId == userId);
                user.Status = "Requested";
                Console.WriteLine(user.UserId);
                Console.WriteLine(user.Status);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
