using System;
using System.Threading.Tasks;

namespace PortfolioSmarts.PortfolioApp.PortfolioDefinition
{
	public class PortfolioDefinitionConfiguration : IPortfolioDefinitionConfiguration
	{
		public Task<string> GetPortfolioDefinitionFilePath()
		{
			Console.WriteLine("Enter the file path of the portfolio definition file:");
			return Task.Run(() => Console.ReadLine());
		}
	}
}
