using System.Collections.Generic;

namespace PortfolioSmarts.Domain.Portfolio
{
	public class PortfolioDefinition
	{
		public string Name { get; set; }
		public IEnumerable<ServiceDefinition> Services { get; set; }
	}
}
