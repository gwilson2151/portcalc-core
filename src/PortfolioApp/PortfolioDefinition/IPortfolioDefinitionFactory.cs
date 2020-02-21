using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public interface IPortfolioDefinitionFactory
	{
		Task<IPortfolioDefinitionDeserialiser> GetDeserialiser();
	}
}
