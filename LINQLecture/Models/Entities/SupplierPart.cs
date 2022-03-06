using System.ComponentModel.DataAnnotations.Schema;

namespace LINQLecture.Models.Entities;

public class SupplierPart
{
    public int Id { get; set; }
    [Column(TypeName = "decimal(6,2)")]
    public decimal Price { get; set; }
    // Navigation properties and foreign keys
    public Part? Part { get; set; }
    public int PartId { get; set; }
    public Supplier? Supplier { get; set; }
    public int SupplierId { get; set; }
}


