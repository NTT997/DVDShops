using DVDShops.Models;

namespace DVDShops.Services.Moviesgenres
{
    public class MovieGenreService : IMovieGenreService
    {
        private DvdshopContext dbContext;
        public MovieGenreService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(MoviesGenre movieGenre)
        {
            try
            {
                dbContext.MoviesGenres.Add(movieGenre);
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
                dbContext.MoviesGenres.Remove(GetById(id));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<MoviesGenre> GetAll()
        {
            return dbContext.MoviesGenres.ToList();
        }

        public List<MoviesGenre> GetByMovieId(int movieId)
        {
            return dbContext.MoviesGenres.Where(gg => gg.MovieId == movieId).ToList();
        }

        public List<MoviesGenre> GetByGenreId(int genreId)
        {
            return dbContext.MoviesGenres.Where(gg => gg.GenreId == genreId).ToList();
        }

        public MoviesGenre GetById(int id)
        {
            return dbContext.MoviesGenres.Find(id);
        }

        public bool Update(MoviesGenre movieGenre)
        {
            try
            {
                dbContext.Entry(movieGenre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
