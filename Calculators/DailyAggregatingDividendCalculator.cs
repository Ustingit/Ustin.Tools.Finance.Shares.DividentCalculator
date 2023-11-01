using System;
using System.Collections.Generic;
using System.Linq;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.Calculators
{
	public class DailyAggregatingDividendCalculator
	{
		private DateTime _startDate;
		private DateTime _endDate;
		private decimal _taxProportion;
		private DividendShare[] _shares;
		private int _initialAmountOfShares;

		public DailyAggregatingDividendCalculator(Period period, decimal taxProportion, Share[] shares)
		{
			_startDate = period.From;
			_endDate = period.To;
			_taxProportion = taxProportion;
			_shares = shares.OfType<DividendShare>().ToArray(); // this exact calculator works only with dividend ones
			_initialAmountOfShares = _shares.Length;
		}

		public void Calculate()
		{
			if (_shares.Length == 0)
			{
				Console.WriteLine("There are no shares.");
				return;
			}

			var currency = _shares[0].Currency;
			var oneSharePrice = _shares[0].Amount;
			var baseShare = _shares[0]; //extremely wrong approach
			decimal totalSum = _shares.Sum(_ => _.Amount);
			decimal notRealizedSum = decimal.Zero;
			Console.WriteLine($"Starting calculating for date range {_startDate.ToShortDateString()} - {_endDate.ToShortDateString()}. Initial sum on balance = {totalSum} {currency.CurrencySign}");

			var currentDate = _startDate;
			var year = currentDate.Year;
			Console.WriteLine($"YEAR {year}:");

			while (currentDate <= _endDate)
			{
				foreach (var dividendShare in _shares)
				{
					if (dividendShare.TryGetDividend(currentDate, out var paidSum))
					{
						notRealizedSum += (paidSum * _taxProportion);
					}
				}

				// assumes that we buy only full shares and we buy only the same shares
				if (notRealizedSum >= oneSharePrice)
				{
					var sharesToBuy = new List<DividendShare>();

					while (notRealizedSum >= oneSharePrice)
					{
						sharesToBuy.Add(new DividendShare()
						{
							Amount = baseShare.Amount,
							Currency = baseShare.Currency,
							CompanyName = baseShare.CompanyName,
							DividendPaymentDates = baseShare.DividendPaymentDates,
							DividendPayment = baseShare.DividendPayment,
							ShareAbbreviation = baseShare.ShareAbbreviation
						});
						notRealizedSum -= oneSharePrice;
					}

					BuyNewShares(sharesToBuy.ToArray());
				}

				var newDate = currentDate.AddDays(1);

				if (newDate.Month != currentDate.Month)
				{
					Console.WriteLine($"At the end of the month {currentDate.Month} of {currentDate.Year} you have {_shares.Sum(_ => _.Amount)} {currency.CurrencySign} in shares and {notRealizedSum} {currency} of unrealized money.");
				}

				if (newDate.Year != currentDate.Year)
				{
					year = newDate.Year;
					Console.WriteLine($"YEAR {year}:");
				}
				currentDate = newDate;
			}

			Console.WriteLine($"End calculation, final sum is {_shares.Sum(_ => _.Amount)} {currency.CurrencySign} and {notRealizedSum} {currency.CurrencySign} of unrealized money.");
		}

		private void BuyNewShares(DividendShare[] newShares)
		{
			_shares = _shares.Concat(newShares).ToArray();
			Console.WriteLine($"Buying {newShares.Length} new shares.");
		}
	}
}
