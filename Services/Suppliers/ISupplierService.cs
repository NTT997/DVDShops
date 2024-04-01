using DVDShops.Models;

namespace DVDShops.Services.Suppliers
{
    public interface ISupplierService
    {
        List<Supplier> GetAll();
        List<Supplier> SearchByName(string supplierName);
        List<Supplier> SearchByEmail(string supplierEmail);
        Supplier GetByName(string supplierName);
        Supplier GetByEmail(string supplierEmail);
        Supplier GetById(int supplierId);
        bool Create(Supplier supplier);
        bool Update(Supplier supplier);
        bool Delete(int supplierId);
    }
}
