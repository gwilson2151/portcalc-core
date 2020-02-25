using System.Threading.Tasks;

namespace PortfolioSmarts.Portfolio.Console.Interfaces
{
	public interface IPortfolioDefinitionConfiguration
	{
		Task<string> GetPortfolioDefinitionFilePath();
	}
}
