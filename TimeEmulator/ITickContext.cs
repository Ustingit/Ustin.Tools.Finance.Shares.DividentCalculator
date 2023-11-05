using System;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator
{
	public interface ITickContext
	{
		public DateTime TickDateTime { get; }
	}
}
