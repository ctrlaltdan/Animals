namespace Animals.Application.User
{
    public interface IUserService
    {
        Domain.Users.User GetUserById(string userId);
    }
}
