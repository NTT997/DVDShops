using DVDShops.Models;
using Humanizer.Localisation;

namespace DVDShops.Services.Genres
{
    public class GenreService : IGenreService
    {

        private DvdshopContext dbContext;
        public GenreService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Genre genre)
        {
            try
            {
                dbContext.Genres.Add(genre);
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
                dbContext.Genres.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Genre> GetAll()
        {
            return dbContext.Genres.ToList();
        }

        public Genre GetById(int id)
        {
            return dbContext.Genres.Find(id);
        }

        public List<Genre> SearchByName(string name)
        {
            return dbContext.Genres.Where(genre => genre.GenreName.ToLower().Contains(name.ToLower())).ToList();
        }

        public bool Update(Genre genre)
        {
            try
            {
                dbContext.Entry(genre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
