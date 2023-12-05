using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(string userid);
        User GetUserByEmail(string email);
        ResultModel AddUser(User user);
        ResultModel DeleteUser(string userid);
        ResultModel EditUser(UserwithPWDDTO userdto);
        User ValidteUser(string email, string password);
        ResultModel UpgradeUser(string userId, string adminId);
        ResultModel BlockUser(string userId, string adminId);
    }
}
