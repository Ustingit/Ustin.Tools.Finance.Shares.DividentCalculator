using System;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models.Common.TIme;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.TimeEmulator
{
	public class TimeEmulator<TOperationContext, TTickContext, TTickResult> 
		where TTickResult : new()
		where TOperationContext : ITimerContext<TTickResult>
		where TTickContext : ITickContext
	{
		private readonly FractionedPeriod _period;
		private readonly TOperationContext _operationalContext;

		private Func<TTickContext, TTickResult> _defaultOnTick = delegate(TTickContext context)
		{
			Console.WriteLine($"Empty iteration {context.TickDateTime.ToShortDateString()}.");
			return default;
		};

		public TimeEmulator(FractionedPeriod period, TOperationContext context)
		{
			_period = period ?? throw new ArgumentNullException(nameof(period));
			_operationalContext = context ?? throw new ArgumentNullException(nameof(context));
		}

		public void Start()
		{
			_operationalContext.Before(_period);
			foreach (var tick in _period)
			{
				_operationalContext?.OnTick(_operationalContext.GetTickContext(tick));
			}
			_operationalContext.After(_period);
		}
	}
}
