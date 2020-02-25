using System.Threading.Tasks;

namespace PortfolioSmarts.Domain.Portfolio.Interfaces
{
	public interface IPortfolioDefinitionFactory
	{
		Task<IPortfolioDefinitionDeserialiser> GetDeserialiser();
	}
}
