using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Services
{
    public interface IFollowerService
    {
        void SendFollowRequest(string userId, string followerId);
        List<User> GetPendingFollowRequests(string userId);
        List<User> GetFollowers(string userId);
        void AcceptFollowRequest(string userId, string followerId);
        Boolean CheckFollowing(string userId, string followerId);
        List<User> GetFollowing(string userId);
    }
}
