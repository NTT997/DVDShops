using DVDShops.Models;

namespace DVDShops.Services.Advertises
{
    public class AdService : IAdService
    {
        private DvdshopContext dbContext;
        public AdService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
