using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public interface IPortfolioDefinitionConfiguration
	{
		Task<string> GetPortfolioDefinitionFilePath();
	}
}
