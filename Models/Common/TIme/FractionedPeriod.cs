using System;
using System.Collections.Generic;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme
{
	public class FractionedPeriod : Period
	{
		private DateTime[] _periodDates;

		public FractionedPeriod(DateTime from, DateTime to) 
			: base(from, to)
		{
			From = from;
			To = to;
			_periodDates = null;
		}

		public DateTime From { get; private set; }

		public DateTime To { get; private set; }

		public DateTime[] PeriodDatesByType(PeriodType type = PeriodType.Month)
		{
			if (_periodDates != null)
			{
				return _periodDates;
			}

			var currentDate = From;
			var result = new List<DateTime>();
			result.Add(currentDate);

			while (currentDate < To)
			{
				DateTime newDate;

				switch (type)
				{
					case PeriodType.Second:
						newDate = currentDate.AddSeconds(1);
						break;
					case PeriodType.Minute:
						newDate = currentDate.AddMinutes(1);
						break;
					case PeriodType.Hour:
						newDate = currentDate.AddHours(1);
						break;
					case PeriodType.HalfDay:
						newDate = currentDate.AddHours(12);
						break;
					case PeriodType.Day:
						newDate = currentDate.AddDays(1);
						break;
					case PeriodType.Week:
						newDate = currentDate.AddDays(7);
						break;
					case PeriodType.Month:
						newDate = currentDate.AddMonths(1);
						break;
					case PeriodType.Quarter:
						newDate = currentDate.AddMonths(3);
						break;
					case PeriodType.HalfYear:
						newDate = currentDate.AddMonths(6);
						break;
					case PeriodType.Year:
						newDate = currentDate.AddYears(1);
						break;
					default:
						throw new InvalidOperationException(nameof(type));
				}

				result.Add(newDate);
				currentDate = newDate;
			}

			result.Add(To);

			_periodDates = result.ToArray();
			return _periodDates;
		}
	}

	public class Period
	{
		public Period(DateTime from, DateTime to)
		{
			From = from;
			To = to;
		}

		public DateTime From { get; private set; }

		public DateTime To { get; private set; }
	}

	public enum PeriodType
	{
		Second = 0,
		Minute = 1,
		Hour = 2,
		HalfDay = 4,
		Day = 8,
		Week = 16,
		Month = 32,
		Quarter = 64,
		HalfYear = 128,
		Year = 256
	}
}
