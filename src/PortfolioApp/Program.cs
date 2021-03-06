﻿using System;
using System.Threading.Tasks;
using PortfolioSmarts.Portfolio.Console;
using PortfolioSmarts.PortfolioApp.Portfolio;
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
			Action initialiseQuestrade = () =>
			{
				program.InitialiseApi().Wait();
				initialiseQuestrade = () => {};
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
					var factory = new PortfolioDefinitionFactory("file", new PortfolioDefinitionConfiguration());
					var processManager = new PortfolioProcessManager(factory);
					var task = processManager.GetPortfolioDefinition();
					task.Wait();
					var portfolioDefinition = task.Result;
					Console.WriteLine(portfolioDefinition.Name);
					Console.WriteLine(portfolioDefinition.Services);
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
