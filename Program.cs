using System;
using Ustin.Tools.Finance.Shares.DividentCalculator.Extensions;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;
using Ustin.Tools.Finance.Shares.DividentCalculator.StubData;
using Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator;
using Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator.Contexts;
using Ustin.Tools.TimeMachine.Models.Ticks;
using Ustin.Tools.TimeMachine.Models.Time;
using Ustin.Tools.TimeMachine.TimeIterator;

namespace Ustin.Tools.Finance.Shares.DividentCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"--> Starting at {DateTime.Now.ToShortDateString()}.");

			var monthlyFractioned4YearsPeriod =
				new FractionedPeriod(DateTime.Now, DateTime.Now.AddMonths(4));

			var share = ShareSamples.GetByShareAbbr("O", monthlyFractioned4YearsPeriod.PeriodDatesByType(PeriodType.Month)) as DividendShare;
			var shares = share.MultiplyDividendOne(20);

			//var calculator = new DailyAggregatingDividendCalculator(new Period(DateTime.Now, DateTime.Now.AddYears(3)), 
			//	(decimal)0.6,
			//	shares);
			//calculator.Calculate();

			var timer = new TimeMachineV1<DividendAggregatorV1, SimpleTickContext, object>(monthlyFractioned4YearsPeriod, 
				new DividendAggregatorV1(shares, (decimal)0.6));
			timer.Start();

			Console.WriteLine($"--> Ending at {DateTime.Now.ToShortDateString()}.");
			Console.ReadKey();
		}
	}
}
