using System.Collections.Generic;
using System.Threading.Tasks;

using PortfolioSmarts.Domain.Portfolio;

namespace PortfolioSmarts.Questrade.Interfaces {
	public interface IQuestradeApi
	{
		Task<IEnumerable<Account>> GetAccountsAsync();
		Task<IEnumerable<Position>> GetPositionsAsync(Account account);
		Task<IEnumerable<Balance>> GetBalancesAsync(Account account);
	}
}