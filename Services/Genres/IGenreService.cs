using DVDShops.Models;

namespace DVDShops.Services.Genres
{
    public interface IGenreService
    {
        List<Genre> GetAll();
        Genre GetById(int id);
        List<Genre> SearchByName(string name);
        bool Create(Genre genre);
        bool Update(Genre genre);
        bool Delete(int id);
    }
}
