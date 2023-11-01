using System.Linq;
using Ustin.Tools.Finance.Shares.DividentCalculator.Models;

namespace Ustin.Tools.Finance.Shares.DividentCalculator.Extensions
{
	public static class ShareExtensions
	{
		public static DividendShare[] MultiplyDividendOne(this DividendShare share, int times)
		{
			return Enumerable.Range(1, times).Select(_ => share.Clone() as DividendShare).ToArray();
		}
	}
}
