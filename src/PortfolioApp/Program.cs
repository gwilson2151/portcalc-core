using System;
using System.Threading.Tasks;
using PortfolioSmarts.Questrade;

namespace PortfolioSmarts.PortfolioApp
{
	class Program
	{
		private readonly QuestradeApi _api;
		private readonly PortfolioService _portfolioService;
		
		private Program()
		{
			_api = new QuestradeApi(new QuestradeClient());
			_portfolioService = new PortfolioService(_api);
		}

		private static void Main(string[] args)
		{
			char op;
			var program = new Program();
			program.InitialiseApi().Wait();

			do {
				Console.WriteLine("Perform an operation by pressing its key:");
				Console.WriteLine("  [S]how Accounts");
				Console.WriteLine("  Calculate [W]eights");
				Console.WriteLine("  E[x]it");

				op = Console.ReadKey(true).KeyChar;

				if (op == 's') {
					var task = program._portfolioService.ShowAccountsAsync();
					task.Wait();
					Console.WriteLine(task.Result);
				} else if (op == 'w') {
					var task = program._portfolioService.CalculateWeightsAsync();
					task.Wait();
					Console.WriteLine(task.Result);
				} else if (op == 'x') {
					Console.WriteLine("Exiting.");
				} else {
					Console.WriteLine($"No operation for [{op}].");
				}

				Console.WriteLine();
			} while (op != 'x');
		}

		private async Task InitialiseApi()
		{
			Console.Write("Enter refresh token: ");
			var refreshToken = Console.ReadLine();
			// todo - ensure non-null, non-empty, non-whitespace token
			await _api.Initialise(refreshToken);
			Console.WriteLine("Initialisation done.");
		}
	}
}
