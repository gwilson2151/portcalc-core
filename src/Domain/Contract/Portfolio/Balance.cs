using PortfolioSmarts.Domain.Contract.Enumerations;
using PortfolioSmarts.Domain.Contract.Interfaces;

namespace PortfolioSmarts.Domain.Contract.Portfolio
{
	public class Balance : IDomainEntity
	{
		public long Id { get; set; }
		public decimal Amount { get; set;}
		public Currency Currency { get; set;}

		public Balance() {}
	}
}
