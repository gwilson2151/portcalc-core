using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioSmarts.Domain.Contract.Portfolio;

namespace PortfolioSmarts.Domain.Service
{
	public interface IService
	{
		Task Initialise(ServiceDefinition serviceDefinition);
		Task<IEnumerable<ServiceAccount>> GetAccountsAsync();
		Task<IEnumerable<Balance>> GetBalancesAsync(ServiceAccount account);
		Task<IEnumerable<Position>> GetPositionsAsync(ServiceAccount account);
	}
}
