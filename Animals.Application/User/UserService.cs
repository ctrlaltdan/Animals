using Animals.Domain.Users;

namespace Animals.Application.User
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        Domain.Users.User IUserService.GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }
    }
}
