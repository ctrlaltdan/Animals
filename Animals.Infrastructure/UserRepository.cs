using System.Linq;
using Animals.Domain.Users;

namespace Animals.Infrastructure
{
    public sealed class UserRepository : IUserRepository
    {
        User IUserRepository.GetById(string userId)
        {
            if (Db.Users.All(u => u.UserId != userId))
                Db.Users.Add(new User(userId));

            return Db.Users.Single(u => u.UserId == userId);
        }
    }
}