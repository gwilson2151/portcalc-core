using System.Diagnostics;
using PortfolioSmarts.Domain.Interfaces;
using PortfolioSmarts.Domain.Portfolio;

namespace PortfolioSmarts.Domain.Analysis
{
	[DebuggerDisplay("{Value.Name, Weight}")]
	public class CategoryWeight : IDomainEntity
	{
		public long Id { get; set; }
		public CategoryValue Value { get; set; }
		public decimal Weight { get; set; }
		public Security Security { get; set; }
	}
}
