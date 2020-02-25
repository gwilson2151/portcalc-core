using System.Collections.Generic;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public class PortfolioDefinition
	{
		public string Name { get; set; }
		public IEnumerable<ServiceDefinition> Services { get; set; }
	}
}
