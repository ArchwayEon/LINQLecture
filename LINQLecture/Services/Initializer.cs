using LINQLecture.Models.Entities;

namespace LINQLecture.Services;

public class Initializer
{
	private readonly ApplicationDbContext _db;

	public Initializer(ApplicationDbContext db)
	{
		_db = db;
	}

	public void SeedDatabase()
	{
		_db.Database.EnsureCreated();

		// Check if the database was already seeded
		if (_db.Suppliers.Any()) return;

		var suppliers = new Supplier[]
		{
		new Supplier { Name = "Acme" },
		new Supplier { Name = "Zappo" },
		new Supplier { Name = "Creepy" }
		};
		_db.Suppliers.AddRange(suppliers);
		_db.SaveChanges();

		var parts = new Part[]
		{
		new Part { Name = "Catapult" },
		new Part { Name = "Zipper" },
		new Part { Name = "Nail" },
		new Part { Name = "Screw" }
		};
		_db.Parts.AddRange(parts);
		_db.SaveChanges();

		var supplier = _db.Suppliers.First(s => s.Name == "Acme");
		var part = _db.Parts.First(p => p.Name == "Catapult");
		var supplierPart = new SupplierPart { SupplierId = supplier.Id, PartId = part.Id, Price = 200.0m };
		_db.SupplierParts.Add(supplierPart);

		part = _db.Parts.First(p => p.Name == "Nail");
		supplierPart = new SupplierPart { SupplierId = supplier.Id, PartId = part.Id, Price = 12.0m };
		_db.SupplierParts.Add(supplierPart);

		supplier = _db.Suppliers.First(s => s.Name == "Zappo");
		part = _db.Parts.First(p => p.Name == "Zipper");
		supplierPart = new SupplierPart { SupplierId = supplier.Id, PartId = part.Id, Price = 2.0m };
		_db.SupplierParts.Add(supplierPart);

		part = _db.Parts.First(p => p.Name == "Nail");
		supplierPart = new SupplierPart { SupplierId = supplier.Id, PartId = part.Id, Price = 8.0m };
		_db.SupplierParts.Add(supplierPart);

		part = _db.Parts.First(p => p.Name == "Screw");
		supplierPart = new SupplierPart { SupplierId = supplier.Id, PartId = part.Id, Price = 8.0m };
		_db.SupplierParts.Add(supplierPart);

		_db.SaveChanges();
	}
}


