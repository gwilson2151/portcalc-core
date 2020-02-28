using System;
using Microsoft.Extensions.DependencyInjection;
using PortfolioSmarts.Domain.Portfolio.Interfaces;
using PortfolioSmarts.Portfolio.Console;
using PortfolioSmarts.Portfolio.Console.Interfaces;
using PortfolioSmarts.PortfolioApp.Portfolio;
using PortfolioSmarts.PortfolioApp.Services;
using PortfolioSmarts.Questrade;
using PortfolioSmarts.Questrade.Interfaces;

namespace PortfolioSmarts.PortfolioApp
{
	class Program
	{
		private static IServiceProvider _serviceProvider;

		private static void Main(string[] args)
		{
			RegisterServices();

			try
			{
				char op;
				var portfolioService = _serviceProvider.GetService<PortfolioService>();

				do
				{
					Console.WriteLine("Perform an operation by pressing its key:");
					Console.WriteLine("  [L]oad Portfolio Definition");
					Console.WriteLine("  [S]how Accounts");
					Console.WriteLine("  Calculate [W]eights");
					Console.WriteLine("  E[x]it");

					op = Console.ReadKey(true).KeyChar;

					if (op == 's' || op == 'S')
					{
						var task = portfolioService.ShowAccountsAsync();
						task.Wait();
						Console.WriteLine(task.Result);
					}
					else if (op == 'w' || op == 'W')
					{
						var task = portfolioService.CalculateWeightsAsync();
						task.Wait();
						Console.WriteLine(task.Result);
					}
					else if (op == 'l' || op == 'L')
					{
						var processManager = _serviceProvider.GetService<PortfolioProcessManager>();
						var loadPortfolioTask = processManager.LoadPortfolioDefinition();
						loadPortfolioTask.Wait();
						var portfolioDefinition = loadPortfolioTask.Result;
						Console.WriteLine(portfolioDefinition.Name);
						foreach (var service in portfolioDefinition.Services)
						{
							Console.WriteLine($"{service.Name}[{service.Type}]");
						}
						var loadAccountsTask = processManager.LoadAccounts(portfolioDefinition);
						loadAccountsTask.Wait();
						var serviceAccounts = loadAccountsTask.Result;
						foreach (var serviceAccount in serviceAccounts)
						{
							Console.WriteLine($"account name: {serviceAccount.Account.Name} service name: {serviceAccount.Service?.Name} service type: {serviceAccount.Service?.Type}");
						}
					}
					else if (op == 'x' || op == 'X')
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
				DefinitionFilePath = "test.json"
			});
			collection.AddSingleton<IPortfolioDefinitionConfiguration, PortfolioDefinitionConfiguration>();
			collection.AddSingleton<IPortfolioDefinitionFactory, PortfolioDefinitionFactory>();
			collection.AddSingleton<PortfolioProcessManager>();
			collection.AddTransient<IQuestradeApi, QuestradeApi>();
			collection.AddSingleton<QuestradeClient>();
			collection.AddSingleton<ConsoleQuestradeInitialiser>();
			collection.AddSingleton<ServiceFactory>();
			collection.AddSingleton<PortfolioService>();

			_serviceProvider = collection.BuildServiceProvider();
		}

		private static void DisposeServices()
		{
			if (_serviceProvider != null && _serviceProvider is IDisposable)
			{
				((IDisposable)_serviceProvider).Dispose();
			}
		}
	}
}
