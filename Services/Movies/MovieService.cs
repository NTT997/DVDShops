﻿using DVDShops.Models;

namespace DVDShops.Services.Movies
{
    public class MovieService : IMovieService
    {
        private DvdshopContext dbContext;
        public MovieService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Movie GetById(int movieId)
        {
            return dbContext.Movies.Find(movieId);
        }

        public bool Create(Movie movie)
        {
            try
            {
                dbContext.Movies.Add(movie);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int movieId)
        {
            try
            {
                dbContext.Movies.Remove(GetById(movieId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Movie> GetAll()
        {
            return dbContext.Movies.ToList();
        }

        public bool Update(Movie movie)
        {
            try
            {
                dbContext.Movies.Add(movie);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Movie> GetByTitle(string movieTitle)
        {
            return dbContext.Movies.Where(movie => movie.MovieTitle.ToLower().Contains(movieTitle.ToLower())).ToList();
        }
        public List<Movie> GetByGenre(int genreId)
        {
            return dbContext.Movies.Where(movie => movie.GenreId == genreId).ToList();
        }

        public List<Movie> GetByProducer(int producerId)
        {
            return dbContext.Movies.Where(movie => movie.ProducerId == producerId).ToList();
        }
    }
}
