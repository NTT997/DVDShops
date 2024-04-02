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
        public Supplier GetByEmail(string supplierEmail)
        {
            supplierEmail = supplierEmail.ToLower();
            return dbContext.Suppliers.SingleOrDefault(s => s.SupplierEmail.ToLower() == supplierEmail);
        }
        public List<Supplier> SearchByEmail(string supplierEmail)
        {
            supplierEmail = supplierEmail.ToLower();
            return dbContext.Suppliers.Where(supplier => supplier.SupplierEmail.ToLower().Contains(supplierEmail)).ToList();
        }
        public Supplier GetByName(string supplierName)
        {
            supplierName = supplierName.ToLower();
            return dbContext.Suppliers.SingleOrDefault(supplier => supplier.SupplierName.ToLower() == supplierName);
        }
        public List<Supplier> SearchByName(string supplierName)
        {
            supplierName = supplierName.ToLower();
            return dbContext.Suppliers.Where(supplier => supplier.SupplierName.ToLower().Contains(supplierName)).ToList();
        }
        public bool Update(Supplier supplier)
        {
            try
            {
                dbContext.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
