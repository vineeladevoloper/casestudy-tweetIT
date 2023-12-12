using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(string userid);
        List<User> GetUsersByName(string searchTerm);
        ResultModel AddUser(User user);
        ResultModel EditUser(UserwithPWDDTO userdto);
        User ValidteUser(string email, string password);
        ResultModel UpgradeUserRequest(string userId, string adminId);
        ResultModel RejectUserRequest(string userId, string adminId);
        ResultModel BlockUser(string userId, string adminId);
        void UpgradeRequest(string userId);
    }
}
