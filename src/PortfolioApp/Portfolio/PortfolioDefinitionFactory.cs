using System.Threading.Tasks;
using PortfolioSmarts.Domain.Portfolio;
using PortfolioSmarts.Domain.Portfolio.Interfaces;
using PortfolioSmarts.Portfolio.Console.Interfaces;
using PortfolioSmarts.Portfolio.FileSystem;

namespace PortfolioSmarts.PortfolioApp.Portfolio
{
	public class PortfolioDefinitionFactory : IPortfolioDefinitionFactory
	{
		private readonly ProgramConfiguration _programConfig;
		private readonly IPortfolioDefinitionConfiguration _configuration;

		public PortfolioDefinitionFactory(ProgramConfiguration programConfig, IPortfolioDefinitionConfiguration configuration)
		{
			_programConfig = programConfig;
			_configuration = configuration;
		}

		public async Task<IPortfolioDefinitionDeserialiser> GetDeserialiser()
		{
			if (_programConfig.DefinitionLoaderType == "file")
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
