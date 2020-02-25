using System.Threading.Tasks;
using PortfolioSmarts.Domain.Portfolio;
using PortfolioSmarts.Domain.Portfolio.Interfaces;
using PortfolioSmarts.Portfolio.Console.Interfaces;
using PortfolioSmarts.Portfolio.FileSystem;

namespace PortfolioSmarts.PortfolioApp.Portfolio
{
	public class PortfolioDefinitionFactory : IPortfolioDefinitionFactory
	{
		private readonly string _type;
		private readonly IPortfolioDefinitionConfiguration _configuration;

		public PortfolioDefinitionFactory(string type, IPortfolioDefinitionConfiguration configuration)
		{
			_type = type;
			_configuration = configuration;
		}

		public async Task<IPortfolioDefinitionDeserialiser> GetDeserialiser()
		{
			if (_type == "file")
			{
				var filePath = await _configuration.GetPortfolioDefinitionFilePath();
				return GetFileDeserialiser(filePath);
			}

			throw new System.NotImplementedException();
		}

		private IPortfolioDefinitionDeserialiser GetFileDeserialiser(string filePath)
		{
			var loader = new FileSystemLoader(filePath);
			return new PortfolioDefinitionJsonDeserialiser(loader);
		}
	}
}
