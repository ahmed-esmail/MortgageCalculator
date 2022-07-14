using System.ComponentModel.DataAnnotations;

namespace CSharpMortgageCalculator.Models
{
  public class Loan
  {
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public decimal Rate { get; set; }
    [Required]
    public int Term { get; set; }
    public decimal Payment { get; set; }
    public decimal TotalInterest { get; set; }
    public decimal TotalCost { get; set; }

    public List<LoanPayment> Payments { get; set; } = new List<LoanPayment>();
  }
}
