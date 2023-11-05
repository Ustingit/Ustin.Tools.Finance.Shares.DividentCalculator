using System;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator
{
	public interface ITimerContext<out TTickResult> 
		where TTickResult : new()
	{
		TTickResult OnTick(ITickContext tickContext);
		
		ITickContext GetTickContext(DateTime tickTime);

		void Before(Period period);

		void After(Period period);
	}
}
