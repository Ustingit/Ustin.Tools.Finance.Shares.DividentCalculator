using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ustin.Tools.Finance.Shares.DividentCalculator.Currencies;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.Models
{
	public class Share
	{
		public string CompanyName { get; set; }

		public string ShareAbbreviation { get; set; }

		public Currency Currency { get; set; }

		public decimal Price { get; set; }
	}
	
	public class DividendShare : Share, ICloneable
	{
		public decimal DividendPayment { get; set; }

		public DateTime[] DividendPaymentDates { get; set; }

		public bool TryGetDividend(DateTime day, out decimal dividendPaid)
		{
			var today = day.ToShortDateString();
			if (DividendPaymentDates.Any(d => d.ToShortDateString() == today))
			{
				dividendPaid = DividendPayment;
				return true;
			}

			dividendPaid = decimal.Zero;
			return false;
		}

		public object Clone()
		{
			return new DividendShare()
			{
				Price = Price,
				CompanyName = CompanyName,
				Currency = Currency,
				DividendPayment = DividendPayment,
				ShareAbbreviation = ShareAbbreviation,
				DividendPaymentDates = DividendPaymentDates
			};
		}
	}
}
