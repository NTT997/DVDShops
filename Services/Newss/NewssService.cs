using DVDShops.Models;

namespace DVDShops.Services.Newss
{
    public class NewssService : INewssService
    {

        private DvdshopContext dbContext;
        public NewssService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(News news)
        {
            try
            {
                dbContext.News.Add(news);
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
                dbContext.News.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<News> GetAll()
        {
            return dbContext.News.ToList();
        }

        public List<News> GetByContent(string newsContent)
        {
            return dbContext.News.Where(news => news.NewsContent.ToLower().Contains(newsContent.ToLower())).ToList();
        }

        public News GetById(int id)
        {
            return dbContext.News.Find(id);
        }

        public List<News> GetBySource(string newsSource)
        {
            return dbContext.News.Where(news => news.NewsSource.ToLower().Contains(newsSource.ToLower())).ToList();
        }

        public List<News> GetByTitle(string newsTitle)
        {
            return dbContext.News.Where(news => news.NewsTitle.ToLower().Contains(newsTitle.ToLower())).ToList();
        }

        public bool Update(News news)
        {
            try
            {
                dbContext.Entry(news).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
