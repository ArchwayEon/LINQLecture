using System.ComponentModel.DataAnnotations;

namespace LINQLecture.Models.Entities;

public class Part
{
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string Name { get; set; } = String.Empty;
    public ICollection<SupplierPart> SuppliedBy { get; set; }
       = new List<SupplierPart>();
}


