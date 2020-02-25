using System.Threading.Tasks;

namespace PortfolioSmarts.Domain.Portfolio.Interfaces
{
	public interface IPortfolioDefinitionDeserialiser
	{
		Task<PortfolioDefinition> Deserialise();
	}
}
