using CSharpMortgageCalculator.Helpers;
using CSharpMortgageCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSharpMortgageCalculator.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }
    [HttpGet]
    public IActionResult App()
    {
      Loan loan = new()
      {
        Payment = 0.0m,
        TotalInterest = 0.0m,
        TotalCost = 0.0m,
        Rate = 3.5m,
        Amount = 15000m,
        Term = 60
      };

      return View(loan);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public IActionResult App([FromForm]Loan loan)
    {
      // calculate the Loan
      var calLoan = LoanHelper.GetPayments(loan);
      return View(calLoan);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}