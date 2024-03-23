using DVDShops.Models;

namespace DVDShops.Services.Suppliers
{
    public interface ISupplierService
    {
        List<Supplier> GetAll();
        List<Supplier> GetByName(string supplierName);
        Supplier GetById(int supplierId);
        bool Create(Supplier supplier);
        bool Update(Supplier supplier);
        bool Delete(int supplierId);
    }
}
