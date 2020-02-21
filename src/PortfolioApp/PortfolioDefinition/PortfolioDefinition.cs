using System.Collections.Generic;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public class PortfolioDefinition
	{
		public string Name { get; private set; }
		public IEnumerable<ServiceDefinition> Services { get; private set; }
	}
}
