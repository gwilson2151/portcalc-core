using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition.Loaders
{
	public interface IPortfolioDefinitionLoader
	{
		Task<string> LoadAsync();
	}
}
