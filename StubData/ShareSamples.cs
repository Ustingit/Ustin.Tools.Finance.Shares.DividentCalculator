using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.StubData
{
	public static class ShareSamples
	{
		// just a blue prints of shares i'm interested in
		private static Share[] Shares = new Share[]
		{
			new DividendShare()
			{
				Amount = (decimal)50.95,
				CompanyName = "Real Income",
				Currency = Currencies.Currencies.TryGetUSD(),
				DividendPayment = (decimal)0.256,
				ShareAbbreviation = "O",
				DividendPaymentDates = new Period(DateTime.Now, DateTime.Now.AddYears(10)).PeriodDatesByType(PeriodType.Month)
			}
		};

		public static Share GetByCompanyName(string companyName, DateTime[] overridenDates = null)
		{
			var share = Shares.First(_ => _.CompanyName == companyName);

			if (overridenDates != null && share is DividendShare divShare)
			{
				divShare.DividendPaymentDates = overridenDates;
				return divShare;
			}

			return share;
		}

		public static Share GetByShareAbbr(string abbr, DateTime[] overridenDates = null)
		{
			var share = Shares.First(_ => _.ShareAbbreviation == abbr);

			if (overridenDates != null && share is DividendShare divShare)
			{
				divShare.DividendPaymentDates = overridenDates;
				return divShare;
			}

			return share;
		}
	}
}
