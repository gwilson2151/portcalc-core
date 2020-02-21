using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
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
