using System.Collections.Generic;
using PortfolioSmarts.Domain.Contract.Interfaces;

namespace PortfolioSmarts.Domain.Contract.Portfolio
{
	public class Portfolio : IDomainEntity
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<Account> Accounts { get; set; }

		public Portfolio()
		{
			Accounts = new List<Account>();
		}
	}
}
