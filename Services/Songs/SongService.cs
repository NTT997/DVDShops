﻿using DVDShops.Models;

namespace DVDShops.Services.Songs
{
    public class SongSerivce : ISongService
    {
        private DvdshopContext dbContext;
        public SongSerivce(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Song GetById(int songId)
        {
            return dbContext.Songs.Find(songId);
        }

        public bool Create(Song song)
        {
            try
            {
                dbContext.Songs.Add(song);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int songId)
        {
            try
            {
                dbContext.Songs.Remove(GetById(songId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Song> GetAll()
        {
            return dbContext.Songs.ToList();
        }

        public bool Update(Song song)
        {
            try
            {
                dbContext.Entry(song).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Song GetByName(string songName)
        {
            songName = songName.Trim();
            return dbContext.Songs.Where(a => a.SongName.ToLower() == songName).OrderByDescending(a => a.SongId).FirstOrDefault();
        }

        public List<Song> SearchByName(string songName)
        {
            songName = songName.Trim();
            return dbContext.Songs.Where(a => a.SongName.ToLower().Contains(songName)).ToList();
        }

        public List<Song> GetByProducer(int producerId)
        {
            return dbContext.Songs.Where(a => a.ProducerId == producerId).ToList();
        }

        public List<Song> GetByArtist(int artistId)
        {
            return dbContext.Songs.Where(a => a.ArtistId == artistId).ToList();
        }
    }
}
