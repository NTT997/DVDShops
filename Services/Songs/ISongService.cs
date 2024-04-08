using DVDShops.Models;

namespace DVDShops.Services.Songs
{
    public interface ISongService
    {
        List<Song> GetAll();
        Song GetById(int songId);
        Song GetByName(string songName);
        List<Song> SearchByName(string songName);
        bool Create(Song song);
        bool Update(Song song);
        bool Delete(int songId);
    }
}
