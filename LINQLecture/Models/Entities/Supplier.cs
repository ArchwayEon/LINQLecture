using System.ComponentModel.DataAnnotations;

namespace LINQLecture.Models.Entities;

public class Supplier
{
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string Name { get; set; } = String.Empty;

    public ICollection<SupplierPart> PartsSupplied { get; set; }
       = new List<SupplierPart>();
}


