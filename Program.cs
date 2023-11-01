using System;
using System.Linq;
using Ustin.Tools.Finance.Shares.DividentCalculator.Calculators;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;

namespace Ustin.Tools.Finance.Shares.DividentCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"--> Starting at {DateTime.Now.ToShortDateString()}.");

			var dates = new[]
			{
				DateTime.Parse("2023-10-18T07:22:16.0000000Z"),
				DateTime.Parse("2023-11-18T07:22:16.0000000Z"),
				DateTime.Parse("2023-12-18T07:22:16.0000000Z"),

				DateTime.Parse("2024-01-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-02-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-03-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-04-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-05-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-06-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-07-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-08-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-09-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-10-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-11-18T07:22:16.0000000Z"),
				DateTime.Parse("2024-12-18T07:22:16.0000000Z"),

				DateTime.Parse("2026-01-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-02-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-03-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-04-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-05-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-06-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-07-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-08-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-09-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-10-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-11-18T07:22:16.0000000Z"),
				DateTime.Parse("2026-12-18T07:22:16.0000000Z"),

				DateTime.Parse("2027-01-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-02-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-03-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-04-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-05-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-06-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-07-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-08-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-09-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-10-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-11-18T07:22:16.0000000Z"),
				DateTime.Parse("2027-12-18T07:22:16.0000000Z"),
			};

			var shares = Enumerable.Range(1, 20).Select(_ => new DividendShare()
			{
				Amount = (decimal)50.95,
				CompanyName = "Real Income",
				Currency = "$",
				DividendPayment = (decimal)0.256,
				ShareAbbreviation = "O",
				DividendPaymentDates = dates
			}).ToArray();
			var calculator = new DailyAggregatingDividendCalculator(DateTime.Now, DateTime.Now.AddYears(3), (decimal)0.6, shares);
			calculator.Calculate();

			Console.WriteLine($"--> Ending at {DateTime.Now.ToShortDateString()}.");
			Console.ReadKey();
		}
	}
}
