using LINQLecture.Models.Entities;

namespace LINQLecture.Services;

public interface ISupplierPartsRepository
{
    IQueryable<SupplierPart> ReadAllSupplierParts();
    IQueryable<Supplier> ReadAllSuppliers();
}


