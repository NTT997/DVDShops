using DVDShops.Models;
using System;

namespace DVDShops.Services.Users
{
    public class UserService : IUserService
    {
        private DvdshopContext dbContext;
        public UserService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
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

        public List<User> GetUsersByActivated(bool activated)
        {
            return dbContext.Users.Where(user => user.UsersActivate == activated).ToList();
        }

        public List<User> GetUsersByEmail(string userEmail)
        {
            return dbContext.Users.Where(user => user.UsersEmail.ToLower().Contains(userEmail.ToLower())).ToList();
        }

        public List<User> GetUsersByName(string userName)
        {
            return dbContext.Users.Where(user => user.UsersName.ToLower().Contains(userName.ToLower())).ToList();
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

        public User GetByName(string userName)
        {
            return dbContext.Users.SingleOrDefault(user => user.UsersName == userName);
        }

        public bool VerifyUser(int userId, string link)
        {
            var account = GetById(userId);
            var verifyLink = $"verifyaccount-{account.UsersName}";

            if (verifyLink == link)
            {
                account.UsersActivate = true;
            }
            return Update(account);
        }
    }
}
