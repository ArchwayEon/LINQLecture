using LINQLecture.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LINQLecture.Services;

public class DbSupplierPartsRepository : ISupplierPartsRepository
{
	private readonly ApplicationDbContext _db;

	public DbSupplierPartsRepository(ApplicationDbContext db)
	{
		_db = db;
	}

	public IQueryable<SupplierPart> ReadAllSupplierParts()
	{
		return _db.SupplierParts
		  .Include(sp => sp.Supplier)
		  .Include(sp => sp.Part);
	}

	public IQueryable<Supplier> ReadAllSuppliers()
	{
		return _db.Suppliers
		   .Include(s => s.PartsSupplied)
		   .ThenInclude(sp => sp.Part);
	}

}


