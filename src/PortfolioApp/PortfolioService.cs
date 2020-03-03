using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioSmarts.Domain.Contract.Portfolio;
using PortfolioSmarts.Domain.Portfolio;
using PortfolioSmarts.Questrade.Interfaces;

namespace PortfolioSmarts.PortfolioApp
{
	public class PortfolioService
	{
		private readonly IQuestradeApi _questradeApi;
		private PortfolioDefinition _definition;

		public PortfolioService(IQuestradeApi questradeApi) => _questradeApi = questradeApi;

		public void SetDefinition(PortfolioDefinition definition) => _definition = definition;

		public async Task<string> ShowAccountsAsync()
		{
			var accounts = await LoadAllPositionsAndBalances(await _questradeApi.GetAccountsAsync());
			return await ShowAccountsAsync(accounts);
		}

		public Task<string> ShowAccountsAsync(IEnumerable<Account> accounts)
		{
			System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();
			resultBuilder.AppendLine($"Accounts{Environment.NewLine}--------");
			foreach (var account in accounts)
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				var total = 0M;
				foreach (var position in account.Positions)
				{
					decimal? currentValue = null;
					if (position.ExtraData.TryGetValue("CurrentValue", out var outVar))
					{
						currentValue = Convert.ToDecimal(outVar);
						total += currentValue.Value;
					}
					decimal? currentPrice = null;
					if (position.ExtraData.TryGetValue("CurrentPrice", out var outPrice))
					{
						currentPrice = Convert.ToDecimal(outPrice);
					}
					var valStr = currentValue != null ? currentValue.Value.ToString("F2") : "--";
					var priceStr = currentPrice != null ? currentPrice.Value.ToString("F2") : "--";
					sb.AppendLine($"  {position.Security.Symbol} - {position.Shares} x {priceStr} = {valStr}");
				}

				foreach (var balance in account.Balances)
				{
					if (balance.Amount > 0M)
					{
						total += balance.Amount;
						sb.AppendLine($"  {balance.Currency.ToString()}    = {balance.Amount.ToString("F2")}");
					}
				}

				resultBuilder.AppendLine($"{account.ExternalId} - {account.Name} = {total.ToString("F2")}");
				resultBuilder.AppendLine(sb.ToString());
			}

			return Task.FromResult(resultBuilder.ToString());
		}

		public async Task<string> CalculateWeightsAsync()
		{
			var accounts = await LoadAllPositionsAndBalances(await _questradeApi.GetAccountsAsync());
			return await CalculateWeightsAsync(accounts);
		}

		public async Task<string> CalculateWeightsAsync(IEnumerable<Account> accounts)
		{
			System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();
			var total = 0M;
			var loader = new HardCodedLoader();
			var categories = await loader.LoadCategories();
			var weightsTask = loader.LoadWeights(categories);

			var portfolioCategory = categories.Where(c => c.Id == HardCodedLoader.PortfolioCategoryId).Single();
			var weightCalc = portfolioCategory.Values.ToDictionary(v => v, v => 0M);

			var weights = await weightsTask;
			foreach (var account in accounts)
			{
				foreach (var position in account.Positions)
				{
					decimal currentValue;
					if (position.ExtraData.TryGetValue("CurrentValue", out var outVar))
					{
						currentValue = Convert.ToDecimal(outVar);
					}
					else
					{
						resultBuilder.AppendLine($"{position.Security.Symbol} has no value in {position.Account.Name}.");
						continue;
					}
					var weight = weights[position.Security.Symbol].Where(w => w.Value.Category == portfolioCategory).Single();
					weightCalc[weight.Value] = weightCalc[weight.Value] + currentValue;
					total += currentValue;
				}

				foreach (var balance in account.Balances)
				{
					var weight = weights[balance.Currency.ToString()].Where(w => w.Value.Category == portfolioCategory).Single();
					weightCalc[weight.Value] = weightCalc[weight.Value] + balance.Amount;
					total += balance.Amount;
				}
			}

			foreach (var kvp in weightCalc)
			{
				resultBuilder.AppendLine($"{kvp.Key.Name,-20} - {kvp.Value.ToString("F2"),9} - {(kvp.Value / total).ToString("P"),6}");
			}
			resultBuilder.AppendLine($"Total                - {total.ToString("F2"),9}");

			return resultBuilder.ToString();
		}

		private async Task<IEnumerable<Account>> LoadAllPositionsAndBalances(IEnumerable<Account> accounts)
		{
			var accountsWithPositions = accounts.Select(a => LoadPositions(a));
			var accountsWithPositionsAndBalances = await Task.WhenAll(accountsWithPositions.Select(async a => LoadBalances(await a)));
			return await Task.WhenAll(accountsWithPositionsAndBalances);
		}

		private async Task<Account> LoadPositions(Account account)
		{
			var loadedAccount = new Account(account);

			var positions = await _questradeApi.GetPositionsAsync(loadedAccount);
			loadedAccount.Positions = loadedAccount.Positions.Concat(positions);

			return loadedAccount;
		}

		private async Task<Account> LoadBalances(Account account)
		{
			var loadedAccount = new Account(account);

			var balances = await _questradeApi.GetBalancesAsync(loadedAccount);
			loadedAccount.Balances = loadedAccount.Balances.Concat(balances);

			return loadedAccount;
		}
	}
}