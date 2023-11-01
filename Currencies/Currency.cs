using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.Currencies
{
	public class Country
	{
		public string FullName { get; set; }

		public string ShortName { get; set; }

		public CountryDevelopmentType DevelopmentStage { get; set; }

		public Currency Currency { get; set; }

		public CountryCumulativeTax CumulativeTax { get; set; }
	}

	/// <summary>
	/// Country may have a different taxes so this class represents the cumulative amount of all the taxes
	/// </summary>
	public class CountryCumulativeTax
	{
		// Ordered "ladder" of taxes
		public decimal[][] TaxesLadder { get; set; }

		/// <summary>
		/// Calculates final amount of tax (in absolute numbers regardless of currency) for a brutto sum
		/// </summary>
		/// <param name="sumBrutto"></param>
		/// <returns></returns>
		public (decimal WholeTaxSum, decimal NettoSumAfterTaxation) CalculateTax(decimal sumBrutto)
		{
			var result = decimal.Zero;

			var currentSumUnderTaxation = sumBrutto; //if there is more than one taxation layer this sum decreases on each layer
			foreach (var taxLevel in TaxesLadder)
			{
				var taxLevelPercent = taxLevel.Sum(_ => _);
				var taxLevelMoneySum = currentSumUnderTaxation * taxLevelPercent;
				result += taxLevelMoneySum;

				currentSumUnderTaxation = currentSumUnderTaxation - taxLevelMoneySum;
			}

			return (WholeTaxSum: result, NettoSumAfterTaxation: currentSumUnderTaxation);
		}
	}

	public enum CountryDevelopmentType {
		Developed = 0,
		Developing = 1,
		UnderDeveloped = 2
	}

	public class Currency
	{
		public string CurrencyAbbr { get; set; }

		public string CurrencySign { get; set; }

		public decimal LastPrice { get; set; }

		public DateTime? LastPriceDate { get; set; }

		public bool HasPrice => LastPriceDate.HasValue && LastPrice != decimal.Zero;
	}

	public static class Currencies
	{
		public static Currency[] AllAvailableCurrencies = new Currency[]
		{
			new Currency()
			{
				CurrencyAbbr = "USD",
				CurrencySign = "$",
				LastPriceDate = DateTime.Now,
				LastPrice = 1
			},
			new Currency()
			{
				CurrencyAbbr = "Euro",
				CurrencySign = "€",
				LastPriceDate = DateTime.Now,
				LastPrice = (decimal)1.05
			},
			new Currency()
			{
				CurrencyAbbr = "PLN",
				CurrencySign = "zł",
				LastPriceDate = DateTime.Now,
				LastPrice = (decimal)4.2265727
			},
		};

		public static Dictionary<string, Currency> AbbrCurrencyMapping => AllAvailableCurrencies
			.ToDictionary(_ => _.CurrencyAbbr, _ => _);
	}
}
