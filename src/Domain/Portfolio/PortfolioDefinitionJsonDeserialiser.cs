using System.Text.Json;
using System.Threading.Tasks;
using PortfolioSmarts.Domain.Portfolio.Interfaces;

namespace PortfolioSmarts.Domain.Portfolio
{
	public class PortfolioDefinitionJsonDeserialiser : IPortfolioDefinitionDeserialiser
	{
		private readonly IPortfolioDefinitionLoader _loader;

		public PortfolioDefinitionJsonDeserialiser(IPortfolioDefinitionLoader loader)
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
