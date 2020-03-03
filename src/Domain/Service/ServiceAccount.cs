using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PortfolioSmarts.Domain.Contract.Portfolio;

namespace PortfolioSmarts.Domain.Service
{
	// This should be an Account that loads Balances and Positions from a service automagically after some amount has time since last refresh
	public class ServiceAccount
	{
		public Account Account { get; set; }
		public ServiceDefinition Service { get; set; }

		public Func<Task<IEnumerable<Balance>>> GetBalancesAsync { get; set; }
		public Func<Task<IEnumerable<Position>>> GetPositionsAsync { get; set; }
	}
}
