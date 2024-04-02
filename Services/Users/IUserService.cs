using DVDShops.Models;

namespace DVDShops.Services.Users
{
    public interface IUserService
    {
        User GetById(int id);
        List<User> GetAll();
        List<User> GetUsersByName(string userProfileName);
        User GetByEmail(string userEmail);
        List<User> GetUsersByEmail(string userEmail);
        List<User> GetUsersByActivated(bool activated);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
        bool VerifyUser(int userId, string verifyLink);
    }
}
