using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PortfolioSmarts.Domain.Portfolio.Interfaces;
using PortfolioSmarts.Portfolio.Console;
using PortfolioSmarts.Portfolio.Console.Interfaces;
using PortfolioSmarts.PortfolioApp.Portfolio;
using PortfolioSmarts.Questrade;

namespace PortfolioSmarts.PortfolioApp
{
	class Program
	{
		private static IServiceProvider _serviceProvider;
		private readonly QuestradeApi _api;
		private readonly PortfolioService _portfolioService;

		private Program()
		{
			_api = new QuestradeApi(new QuestradeClient());
			_portfolioService = new PortfolioService(_api);
		}

		private static void Main(string[] args)
		{
			RegisterServices();

			try
			{
				char op;
				var program = new Program();
				Action initialiseQuestrade = () =>
				{
					program.InitialiseApi().Wait();
					initialiseQuestrade = () => { };
				};

				do
				{
					Console.WriteLine("Perform an operation by pressing its key:");
					Console.WriteLine("  [L]oad Portfolio Definition");
					Console.WriteLine("  [S]how Accounts");
					Console.WriteLine("  Calculate [W]eights");
					Console.WriteLine("  E[x]it");

					op = Console.ReadKey(true).KeyChar;

					if (op == 's')
					{
						initialiseQuestrade();
						var task = program._portfolioService.ShowAccountsAsync();
						task.Wait();
						Console.WriteLine(task.Result);
					}
					else if (op == 'w')
					{
						initialiseQuestrade();
						var task = program._portfolioService.CalculateWeightsAsync();
						task.Wait();
						Console.WriteLine(task.Result);
					}
					else if (op == 'l')
					{
						var processManager = _serviceProvider.GetService<PortfolioProcessManager>();
						var task = processManager.GetPortfolioDefinition();
						task.Wait();
						var portfolioDefinition = task.Result;
						Console.WriteLine(portfolioDefinition.Name);
						foreach (var service in portfolioDefinition.Services)
						{
							Console.WriteLine($"{service.Name}[{service.Type}]");
						}
					}
					else if (op == 'x')
					{
						Console.WriteLine("Exiting.");
					}
					else
					{
						Console.WriteLine($"No operation for [{op}].");
					}

					Console.WriteLine();
				} while (op != 'x');
			}
			finally
			{
				DisposeServices();
			}
		}

		private static void RegisterServices()
		{
			var collection = new ServiceCollection();
			collection.AddSingleton(new ProgramConfiguration
			{
				DefinitionLoaderType = "file",
				DefinitionFilePath = "..\\test.json"
			});
			collection.AddSingleton<IPortfolioDefinitionConfiguration, PortfolioDefinitionConfiguration>();
			collection.AddSingleton<IPortfolioDefinitionFactory, PortfolioDefinitionFactory>();
			collection.AddSingleton<PortfolioProcessManager>();

			_serviceProvider = collection.BuildServiceProvider();
		}

		private static void DisposeServices()
		{
			if (_serviceProvider != null && _serviceProvider is IDisposable)
			{
				((IDisposable)_serviceProvider).Dispose();
			}
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
