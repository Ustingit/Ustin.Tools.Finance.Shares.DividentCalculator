using System;
using System.Collections.Generic;
using System.Linq;
using Ustin.Tools.Finance.Shares.DividentCalculator.Currencies;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;
using Ustin.Tools.TimeMachine.Abstractions;
using Ustin.Tools.TimeMachine.Abstractions.Ticks;
using Ustin.Tools.TimeMachine.Models.Ticks;
using Ustin.Tools.TimeMachine.Models.Time;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator.Contexts
{
	public class DividendAggregatorV1 : ITimerContext<object>
	{
		private readonly decimal _taxProportion;
		private DividendShare[] _shares;

		private readonly Currency _shareCurrency; //TODO: currently works with only one currency (re-work this and below)
		private readonly decimal _oneSharePrice;
		private readonly DividendShare _baseShare;

		private decimal _totalSum;
		private decimal _notRealizedSum;
		private bool _isVeryFirstTick;

		public DividendAggregatorV1(Share[] initialShares, decimal taxProportion)
		{
			_shares = initialShares.OfType<DividendShare>().ToArray(); // this exact calculator works only with dividend ones;
			_taxProportion = taxProportion;
			_isVeryFirstTick = true;

			if (_shares.Length < 1)
			{
				throw new InvalidOperationException("There are no shares");
			}

			_baseShare = _shares[0];
			_shareCurrency = _baseShare.Currency;
			_oneSharePrice = _baseShare.Price;
			_totalSum = _shares.Sum(_ => _.Price);
			_notRealizedSum = decimal.Zero;
		}

		public void Before(Period period)
		{
			Console.WriteLine($"Starting calculating for date range {period.From.ToShortDateString()} - {period.To.ToShortDateString()}. Initial sum on balance = {_totalSum} {_shareCurrency.CurrencySign}");
			Console.WriteLine($"Year: {period.From.Year} =>");
		}

		public object OnTick(ITickContext tickContext)
		{
			foreach (var dividendShare in _shares)
			{
				if (dividendShare.TryGetDividend(tickContext.TickDateTime, out var paidSum))
				{
					_notRealizedSum += (paidSum * _taxProportion);
				}
			}

			// assumes that we buy only full shares and we buy only the same shares
			if (_notRealizedSum >= _oneSharePrice)
			{
				var sharesToBuy = new List<DividendShare>();
				
				while (_notRealizedSum >= _oneSharePrice)
				{
					sharesToBuy.Add(_baseShare.Clone() as DividendShare);
					_notRealizedSum -= _oneSharePrice;
				}

				BuyNewShares(sharesToBuy.ToArray());
			}

			return null;
		}

		private void BuyNewShares(DividendShare[] newShares)
		{
			_shares = _shares.Concat(newShares).ToArray();
			Console.WriteLine($"Buying {newShares.Length} new shares.");
		}

		private void TryDoEndOfYearAndMonthOperation()
		{
			throw new NotImplementedException();
		}

		public ITickContext GetTickContext(DateTime tickTime)
		{
			return new SimpleTickContext(tickTime);
		}

		public void After(Period period)
		{
			Console.WriteLine($"End calculation, final sum is {_shares.Sum(_ => _.Price)} {_shareCurrency.CurrencySign} and {_notRealizedSum} {_shareCurrency.CurrencySign} of unrealized money.");
		}
	}
}
