using System.Threading.Tasks;
using PortfolioSmarts.Portfolio.Console.Interfaces;

namespace PortfolioSmarts.Portfolio.Console
{
	public class PortfolioDefinitionConfiguration : IPortfolioDefinitionConfiguration
	{
		public Task<string> GetPortfolioDefinitionFilePath()
		{
			System.Console.WriteLine("Enter the file path of the portfolio definition file:");
			return Task.Run(() => System.Console.ReadLine());
		}
	}
}
