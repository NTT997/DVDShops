using DVDShops.Models;

namespace DVDShops.Services.Producers
{
    public class ProducerService : IProducerService
    {
        private DvdshopContext dbContext;
        public ProducerService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(Producer producer)
        {
            try
            {
                dbContext.Producers.Add(producer);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int producerId)
        {
            try
            {
                dbContext.Producers.Remove(GetById(producerId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Producer> GetAll()
        {
            return dbContext.Producers.ToList();
        }

        public Producer GetById(int producerId)
        {
            return dbContext.Producers.Find(producerId);
        }

        public Producer GetByName(string producerName)
        {
            producerName = producerName.ToLower();
            return dbContext.Producers.FirstOrDefault(producer => producer.ProducerName.ToLower() == producerName);
        }

        public List<Producer> SearchByName(string producerName)
        {
            producerName = producerName.ToLower();
            return dbContext.Producers.Where(producers => producers.ProducerName.ToLower().Contains(producerName)).ToList();
        }

        public bool Update(Producer producer)
        {
            try
            {
                dbContext.Entry(producer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
