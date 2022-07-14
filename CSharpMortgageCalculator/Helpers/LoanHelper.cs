using CSharpMortgageCalculator.Models;

namespace CSharpMortgageCalculator.Helpers
{
  public class LoanHelper
  {
    public static Loan GetPayments(Loan loan)
    {
      // calculate Monthly Payment
      loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);

      // loop to the term
      var balance = loan.Amount;
      var totalInterest = 0.0m;
      var monthlyRate = CalcMonthlyRate(loan.Rate);

      for (int month = 1; month <= loan.Term; month++)
      {
        decimal monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
        totalInterest += monthlyInterest;
        decimal monthlyPrincipal = loan.Payment - monthlyInterest;
        balance -= monthlyPrincipal;

        var loanPayment = new LoanPayment()
        {
          Month = month,
          Payment = loan.Payment,
          MonthlyPrincipal = monthlyPrincipal,
          MonthlyInterest = monthlyInterest,
          TotalInterest = totalInterest,
          Balance = balance
        };

        loan.Payments.Add(loanPayment);
      }

      loan.TotalInterest = totalInterest;
      loan.TotalCost = loan.Amount + totalInterest;
      // calculate a payment schedule
      return loan;
    }

    private static decimal CalcPayment(decimal amount, decimal rate, int term)
    {
      var monthlyRate = CalcMonthlyRate(rate);

      var rateD = Convert.ToDouble(monthlyRate);
      var amountD = Convert.ToDouble(amount);

      var paymentD = (amountD * rateD) / (1 - Math.Pow((1 + rateD), -term));

      return Convert.ToDecimal(paymentD);
    }

    private static decimal CalcMonthlyRate(decimal rate)
    {
      return rate / 1200;
    }

    private static decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate)
    {
      return balance * monthlyRate;
    }
  }
}
