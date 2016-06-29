namespace Animals.Domain.Users
{
    public interface IUserRepository
    {
        User GetById(string userId);
    }
}
