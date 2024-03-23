using DVDShops.Models;

namespace DVDShops.Services.Newss
{
    public interface INewssService
    {
        News GetById(int id);
        List<News> GetAll();
        List<News> GetByTitle(string newsTitle);
        List<News> GetByContent(string newsContent);
        List<News> GetBySource(string newsSource);
        bool Create(News news);
        bool Update(News news);
        bool Delete(int id);
    }
}
