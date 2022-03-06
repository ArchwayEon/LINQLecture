using LINQLecture.ExtensionClasses;
using LINQLecture.Models;
using LINQLecture.Models.ViewModels;
using LINQLecture.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace LINQLecture.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISupplierPartsRepository _supplierPartsRepo;

    public HomeController(ISupplierPartsRepository supplierPartsRepo, ILogger<HomeController> logger)
    {
        _logger = logger;
        _supplierPartsRepo = supplierPartsRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ExtensionMethodExamples()
    {
        string name = "Jeffrey"; // The middle character is 'f'
                                 // Using the static class with the static method
        char middle1 = StringExtensionsAlmost.MiddleChar(name);
        // Using the static class with the extension method
        // Note that the extension method is called as if it's part
        // of the string class
        char middle2 = name.MiddleChar();
        return Content($"The middle character of {name} is {middle1} {middle2}");
    }

    public IActionResult LambdaExamples()
    {
        Func<double, double> Square = (x) => x * x;
        Func<double, double, double> Pow = (x, y) => Math.Pow(x, y);
        Action HelloWorld = () =>
        {
            _logger.LogInformation("Hello, World!");
        };
        Action<string> GreetWorld = (message) =>
        {
            string greeting = $"{message}, World!";
            _logger.LogInformation(greeting);
        };
        HelloWorld();
        GreetWorld("Hi");
        return Content($"5^2={Square(5)} 2^10={Pow(2, 10)}");
    }

    public IActionResult FilteringEM()
    {
        ViewData["Message"] = "Parts that cost less than 12.0 (Extension Method syntax)";
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        // Extension method syntax
        var model = query.Where(sp => sp.Price < 12.0m);
        return View("PartSupplierPrice", model);
    }
    public IActionResult FilteringQ()
    {
        ViewData["Message"] = "Parts that cost less than 12.0 (Query syntax)";
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        // Query syntax
        var model = from sp in query
                    where sp.Price < 12.0m
                    select sp;
        return View("PartSupplierPrice", model);
    }

    public IActionResult SortingEM()
    {
        ViewData["Message"] = "Parts sorted by price then by name (EM)";
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        // Extension method syntax
        var model = query
           .OrderBy(sp => sp.Price)
           .ThenBy(sp => sp.Part!.Name);
        return View("PartSupplierPrice", model);
    }
    public IActionResult SortingQ()
    {
        ViewData["Message"] = "Parts sorted by price then by name (Q)";
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        var model = from sp in query
                    orderby sp.Price, sp.Part!.Name
                    select sp;
        return View("PartSupplierPrice", model);
    }

    public IActionResult Sets()
    {
        int[] twos = { 0, 2, 4, 6, 8, 10, 12, 14 };
        int[] threes = { 0, 3, 6, 9, 12, 15, 18, 21 };
        var data = new StringBuilder();
        data.Append("Twos: " + string.Join(",", twos) + "\n");
        data.Append("Threes: " + string.Join(",", threes) + "\n");
        data.Append('\n');

        var q1 = twos.Except(threes);
        data.Append("Twos except threes\n");
        data.Append(string.Join(",", q1.ToArray()));
        data.Append('\n');

        var q2 = twos.Intersect(threes);
        data.Append("Twos intersect threes\n");
        data.Append(string.Join(",", q2.ToArray()));
        data.Append('\n');

        return Content(data.ToString());
    }

    public IActionResult Quantifiers()
    {
        var data = new StringBuilder();
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        var check = query.Any(sp => sp.Price > 50.0m);
        if (check)
        {
            data.Append("There is a part with a price greater than 50.0.");
        }
        data.Append('\n');

        int[] threes = { 0, 3, 6, 9, 12, 15, 18, 21 };
        data.Append("Threes: " + string.Join(",", threes));
        check = threes.Contains(14);
        if (!check)
        {
            data.Append(" does not ");
        }
        data.Append("contain 14\n");

        return Content(data.ToString());
    }

    public IActionResult ProjectionEM()
    {
        ViewData["Message"] = "Increase part price by 10% (EM)";
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        var model = query
          .Select(sp =>
              new PartNewPriceVM
              {
                  Name = sp.Part!.Name,
                  OldPrice = sp.Price,
                  NewPrice = sp.Price * 1.1m
              });
        return View("Projection", model);
    }

    public IActionResult ProjectionQ()
    {
        ViewData["Message"] = "Increase part price by 10% (Q)";
        var query = _supplierPartsRepo.ReadAllSupplierParts();
        var model = from sp in query
                    select new PartNewPriceVM
                    {
                        Name = sp.Part!.Name,
                        OldPrice = sp.Price,
                        NewPrice = sp.Price * 1.1m
                    };
        return View("Projection", model);
    }

    public IActionResult SupplierPartsEM()
    {
        ViewData["Message"] = "Supplier supplies Parts (EM)";
        var suppliersQry = _supplierPartsRepo.ReadAllSuppliers();
        var supplierPartsQry = _supplierPartsRepo.ReadAllSupplierParts();
        var model = suppliersQry.Join(
                supplierPartsQry,
                s => s.Id, sp => sp.SupplierId,
                (s, sp) => new SuppliesVM
                {
                    SupplierName = s.Name,
                    PartName = sp.Part!.Name
                }
            );
        return View("SupplierParts", model);
    }

    public IActionResult SupplierPartsQ()
    {
        ViewData["Message"] = "Supplier supplies Parts (Q)";
        var suppliersQry = _supplierPartsRepo.ReadAllSuppliers();
        var supplierPartsQry = _supplierPartsRepo.ReadAllSupplierParts();
        var model = from s in suppliersQry
                    join sp in supplierPartsQry on s.Id equals sp.SupplierId
                    select new SuppliesVM
                    {
                        SupplierName = s.Name,
                        PartName = sp.Part!.Name
                    };

        return View("SupplierParts", model);
    }




    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
