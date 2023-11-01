using System;
using System.Linq;
using Ustin.Tools.Finance.Shares.DividentCalculator.Calculators;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme;

namespace Ustin.Tools.Finance.Shares.DividentCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"--> Starting at {DateTime.Now.ToShortDateString()}.");

			var shares = Enumerable.Range(1, 20).Select(_ => new DividendShare()
			{
				Amount = (decimal)50.95,
				CompanyName = "Real Income",
				Currency = Currencies.Currencies.TryGetUSD(),
				DividendPayment = (decimal)0.256,
				ShareAbbreviation = "O",
				DividendPaymentDates = new Period(DateTime.Now, DateTime.Now.AddYears(4)).PeriodDatesByType(PeriodType.Month)
			}).ToArray();

			var calculator = new DailyAggregatingDividendCalculator(DateTime.Now, DateTime.Now.AddYears(3), (decimal)0.6, shares);
			calculator.Calculate();

			Console.WriteLine($"--> Ending at {DateTime.Now.ToShortDateString()}.");
			Console.ReadKey();
		}
	}
}
