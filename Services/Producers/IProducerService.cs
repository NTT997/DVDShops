using DVDShops.Models;

namespace DVDShops.Services.Producers
{
    public interface IProducerService
    {
        List<Producer> GetAll();
        List<Producer> GetByName(string producerName);
        Producer GetById(int producerID);
        bool Create(Producer producer);
        bool Update(Producer producer);
        bool Delete(int producerID);
    }
}
