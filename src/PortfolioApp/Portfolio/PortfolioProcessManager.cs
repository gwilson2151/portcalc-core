using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioSmarts.Domain.Portfolio;
using PortfolioSmarts.Domain.Portfolio.Interfaces;
using PortfolioSmarts.Domain.Service;
using PortfolioSmarts.PortfolioApp.Services;

namespace PortfolioSmarts.PortfolioApp.Portfolio
{
	public class PortfolioProcessManager
	{
		private readonly IPortfolioDefinitionFactory _factory;
		private readonly ServiceFactory _serviceFactory;

		public PortfolioProcessManager(IPortfolioDefinitionFactory factory, ServiceFactory serviceFactory)
		{
			_factory = factory;
			_serviceFactory = serviceFactory;
		}

		public async Task<PortfolioDefinition> LoadPortfolioDefinition()
		{
			var deserialiser = await _factory.GetDeserialiser();
			return await deserialiser.Deserialise();
		}

		public async Task<IEnumerable<ServiceAccount>> LoadAccounts(PortfolioDefinition definition)
		{
			var services = definition.Services.Select(s => _serviceFactory.GetService(s));
			var tasks = Task.WhenAll(services.Select(s => s.GetAccountsAsync()));
			return (await tasks).SelectMany(sa => sa).ToList();
		}
	}
}
