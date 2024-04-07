using DVDShops.Models;

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
            userEmail = userEmail.Trim();
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
    }
}
