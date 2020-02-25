using System.Diagnostics;
using PortfolioSmarts.Domain.Contract.Interfaces;
using PortfolioSmarts.Domain.Contract.Portfolio;

namespace PortfolioSmarts.Domain.Contract.Analysis
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
