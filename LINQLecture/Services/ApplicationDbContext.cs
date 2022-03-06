using LINQLecture.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LINQLecture.Services;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<SupplierPart> SupplierParts => Set<SupplierPart>();
}

