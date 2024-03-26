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

        public List<Producer> GetByName(string producerName)
        {
            return dbContext.Producers.Where(producers => producers.ProducerName.ToLower().Contains(producerName.ToLower())).ToList();
        }

        public bool Update(Producer producer)
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
    }
}
