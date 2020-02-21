using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public interface IPortfolioDefinitionDeserialiser
	{
		Task<PortfolioDefinition> Deserialise();
	}
}
