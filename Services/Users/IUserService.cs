using DVDShops.Models;

namespace DVDShops.Services.Users
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByEmail(string userEmail);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
        bool VerifyUser(int userId, string verifyLink);
    }
}
