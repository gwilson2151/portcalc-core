using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PortfolioSmarts.Domain.Contract.Portfolio;

namespace PortfolioSmarts.Domain.Service
{
	public class ServiceAccount
	{
		public Account Account { get; set; }
		public ServiceDefinition Service { get; set; }

		public Func<Task<IEnumerable<Balance>>> GetBalancesAsync { get; set; }
		public Func<Task<IEnumerable<Position>>> GetPositionsAsync { get; set; }
	}
}
