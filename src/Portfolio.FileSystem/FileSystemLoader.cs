using System;
using System.IO;
using System.Threading.Tasks;
using PortfolioSmarts.Domain.Portfolio.Interfaces;

namespace PortfolioSmarts.Portfolio.FileSystem
{
	public class FileSystemLoader : IPortfolioDefinitionLoader
	{
		private readonly string _filePath;
		public FileSystemLoader(string filePath)
		{
			_filePath = filePath;
		}

		public async Task<string> LoadAsync()
		{
			var portfolioDefinitionFilePath = Path.GetFullPath(Environment.ExpandEnvironmentVariables(_filePath));
			if (!File.Exists(portfolioDefinitionFilePath))
			{
				throw new FileNotFoundException($"Could not find portfolio definition file at [{_filePath}].", portfolioDefinitionFilePath);
			}

			var reader = new StreamReader(portfolioDefinitionFilePath);
			return await reader.ReadToEndAsync();
		}
	}
}
