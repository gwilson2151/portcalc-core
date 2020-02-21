using System.Text.Json;
using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public class PortfolioDefinitionDeserialiser : IPortfolioDefinitionDeserialiser
	{
		private readonly Loaders.IPortfolioDefinitionLoader _loader;

		public PortfolioDefinitionDeserialiser(Loaders.IPortfolioDefinitionLoader loader)
		{
			_loader = loader;
		}

		public async Task<PortfolioDefinition> Deserialise()
		{
			var json = await _loader.LoadAsync();
			return JsonSerializer.Deserialize<PortfolioDefinition>(json);
		}
	}
}
