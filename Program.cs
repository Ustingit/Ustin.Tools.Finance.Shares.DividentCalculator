using System;
using System.Linq;
using Ustin.Tools.Finance.Shares.DividentCalculator.Calculators;
using Ustin.Tools.Finance.Shares.DividentCalculator.Extensions;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme;
using Ustin.Tools.Finance.Shares.DividentCalculator.StubData;

namespace Ustin.Tools.Finance.Shares.DividentCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"--> Starting at {DateTime.Now.ToShortDateString()}.");

			var share = ShareSamples.GetByShareAbbr("O",
				new Period(DateTime.Now, DateTime.Now.AddYears(4)).PeriodDatesByType(PeriodType.Month)) as DividendShare;
			var shares = share.MultiplyDividendOne(20);

			var calculator = new DailyAggregatingDividendCalculator(DateTime.Now, 
				DateTime.Now.AddYears(3), 
				(decimal)0.6,
				shares);
			calculator.Calculate();

			Console.WriteLine($"--> Ending at {DateTime.Now.ToShortDateString()}.");
			Console.ReadKey();
		}
	}
}
