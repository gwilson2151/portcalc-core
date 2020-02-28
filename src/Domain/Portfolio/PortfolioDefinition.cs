using System.Collections.Generic;
using PortfolioSmarts.Domain.Contract.Portfolio;
using PortfolioSmarts.Domain.Service;

namespace PortfolioSmarts.Domain.Portfolio
{
	public class PortfolioDefinition
	{
		public string Name { get; set; }
		public IEnumerable<ServiceDefinition> Services { get; set; }

		public IEnumerable<Account> Accounts { get; private set; }
	}
}
