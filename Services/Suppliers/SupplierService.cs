using DVDShops.Models;

namespace DVDShops.Services.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private DvdshopContext dbContext;
        public SupplierService(DvdshopContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Create(Supplier supplier)
        {
            try
            {
                dbContext.Suppliers.Add(supplier);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int supplierId)
        {
            try
            {
                dbContext.Suppliers.Remove(GetById(supplierId));
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Supplier> GetAll()
        {
            return dbContext.Suppliers.ToList();
        }

        public Supplier GetById(int supplierId)
        {
            return dbContext.Suppliers.Find(supplierId);
        }

        public List<Supplier> GetByName(string supplierName)
        {
            return dbContext.Suppliers.Where(supplier => supplier.SupplierName.ToLower().Contains(supplierName.ToLower())).ToList();
        }

        public bool Update(Supplier supplier)
        {
            try
            {
                dbContext.Suppliers.Add(supplier);
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
