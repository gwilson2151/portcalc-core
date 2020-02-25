using System.Threading.Tasks;
using PortfolioSmarts.Domain.Portfolio;
using PortfolioSmarts.Domain.Portfolio.Interfaces;

namespace PortfolioSmarts.PortfolioApp.Portfolio
{
	public class PortfolioProcessManager
	{
		private readonly IPortfolioDefinitionFactory _factory;
		public PortfolioProcessManager(IPortfolioDefinitionFactory factory)
		{
			_factory = factory;
		}

		public async Task<PortfolioDefinition> GetPortfolioDefinition()
		{
			var deserialiser = await _factory.GetDeserialiser();
			return await deserialiser.Deserialise();
		}
	}
}
