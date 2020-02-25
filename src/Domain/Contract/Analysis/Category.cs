using System.Collections.Generic;
using System.Diagnostics;
using PortfolioSmarts.Domain.Contract.Interfaces;

namespace PortfolioSmarts.Domain.Contract.Analysis
{
	[DebuggerDisplay("{Name}")]
	public class Category : IDomainEntity
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public ICollection<CategoryValue> Values { get; set; }

		public Category()
		{
			Values = new List<CategoryValue>();
		}
	}
}
