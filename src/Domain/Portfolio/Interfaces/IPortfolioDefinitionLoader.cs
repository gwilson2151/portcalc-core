using System.Threading.Tasks;

namespace PortfolioSmarts.Domain.Portfolio.Interfaces
{
	public interface IPortfolioDefinitionLoader
	{
		Task<string> LoadAsync();
	}
}
