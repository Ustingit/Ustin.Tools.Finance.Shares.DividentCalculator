using System;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator
{
	public class SimpleTickContext : ITickContext
	{
		public SimpleTickContext(DateTime tickDateTime)
		{
			TickDateTime = tickDateTime;
		}

		public DateTime TickDateTime { get; private set; }
	}
}
