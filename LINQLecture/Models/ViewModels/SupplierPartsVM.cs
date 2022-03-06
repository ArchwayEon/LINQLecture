using LINQLecture.Models.Entities;

namespace LINQLecture.Models.ViewModels;

public class SupplierPartsVM
{
    public string? SupplierName { get; set; }
    public IEnumerable<SupplierPart> SupplierParts { get; set; }
        = new List<SupplierPart>();
}


