using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTwitterAPI.Database;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Services
{
    public class FollowerService:IFollowerService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public FollowerService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }
        public void SendFollowRequest(string userId, string followerId)
        {
            try
            {
                var followRequest = new Follower
                {
                    UserId = userId,
                    FollowerId = followerId,
                    Status = 0
                };

                context.Followers.Add(followRequest);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Accept a follow request
        public void AcceptFollowRequest(string userId, string followerId)
        {
            var followRequest = context.Followers
                .SingleOrDefault(f => f.UserId == userId && f.FollowerId == followerId && f.Status == 0);

            if (followRequest != null)
            {
                followRequest.Status = 1;
                context.SaveChanges();
            }
        }

        public Boolean CheckFollowing(string userId, string followerId)
        {
            var followRequest = context.Followers
                .SingleOrDefault(f => f.UserId == userId && f.FollowerId == followerId );

            if(followRequest != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Get followers for a user
        public List<User> GetFollowers(string userId)
        {
            return context.Followers
                .Where(f => f.UserId == userId && f.Status == 1)
                .Select(f => f.FollowerUser)
                .ToList();
        }

        public List<User> GetFollowing(string userId)
        {
            return context.Followers
                .Where(f => f.FollowerId == userId && f.Status == 1)
                .Select(f => f.User)
                .ToList();
        }

        // Get pending follow requests for a user
        public List<User> GetPendingFollowRequests(string userId)
        {
            return context.Followers
                .Where(f => f.UserId == userId && f.Status == 0) // 0 for pending requests
                .Select(f => f.FollowerUser)
                .ToList();
        }
    }
}
