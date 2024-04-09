using DVDShops.Models;
using BCrypt.Net;

namespace DVDShops.Services.Users
{
    public class UserService : IUserService
    {
        private DvdshopContext dbContext;
        private IConfiguration configuration;
        public UserService(DvdshopContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public bool Create(User user)
        {
            try
            {
                dbContext.Users.Add(user);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                dbContext.Users.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return dbContext.Users.Find(id);
        }        

        public bool Update(User user)
        {
            try
            {
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetByEmail(string userEmail)
        {
            
            return dbContext.Users.Where(user => user.UsersEmail == userEmail).OrderByDescending(u => u.UsersId).FirstOrDefault();
        }

        public bool VerifyUser(int userId, string link)
        {
            var account = GetById(userId);
            var baseUrl = configuration["BaseUrl"];
            var verifyLink = $"{baseUrl}/verify?userEmail={account.UsersEmail}";

            if (verifyLink == link)
            {
                account.UsersActivated = true;
            }
            return Update(account);
        }

        public bool Login(string email, string password)
        {
            // Retrieve the user account associated with the given email
            var account = GetByEmail(email);

            // If no account is found, return false
            if (account == null)
            {
                return false;
            }

            // Verify whether the provided password matches the hashed password stored in the user account
            if (!BCrypt.Net.BCrypt.Verify(password, account.UsersPassword))
            {
                // If password verification fails, return false
                return false;
            }

            // If both account retrieval and password verification are successful, return true
            return true;
        }
    }
}
