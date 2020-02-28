using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioSmarts.Domain.Contract.Portfolio;
using PortfolioSmarts.Domain.Contract.Enumerations;
using PortfolioSmarts.Domain.Service;
using PortfolioSmarts.Questrade.Interfaces;

namespace PortfolioSmarts.Questrade
{
	public class QuestradeApi : IQuestradeApi, IService
	{
		private readonly QuestradeClient _client;
		private readonly ConsoleQuestradeInitialiser _serviceInitialiser;
		private SessionState _sessionState;
		private ServiceDefinition _serviceDefinition;

		public QuestradeApi(QuestradeClient client, ConsoleQuestradeInitialiser serviceInitialiser)
		{
			_client = client;
			_serviceInitialiser = serviceInitialiser;
		}

		public async Task Initialise(string refreshToken)
		{
			if (string.IsNullOrWhiteSpace(refreshToken)) {
				throw new ArgumentException("Value cannot be null, empty, or white space.", nameof(refreshToken));
			}
			_sessionState = await _client.Authenticate(refreshToken);
		}

		public async Task<IEnumerable<Account>> GetAccountsAsync()
		{
			await EnsureSessionAuthentication();

			var qAccounts = await _client.GetAccounts(_sessionState);

			return qAccounts.Select(a => new Account {
				ExternalId = a.Number,
				Name = a.Type
			});
		}

		public async Task<IEnumerable<Position>> GetPositionsAsync(Account account)
		{
			await EnsureSessionAuthentication();

			var qPositions = await _client.GetPositions(_sessionState, account.ExternalId);

			return qPositions.Select(p => new Position {
				Account = account,
				Security = new Security {
					Symbol = p.Symbol
				},
				Shares = Convert.ToDecimal(p.OpenQuantity),
				ExtraData = new Dictionary<string, object> {
					{ "CurrentValue", Convert.ToDecimal(p.CurrentMarketValue) },
					{ "CurrentPrice", Convert.ToDecimal(p.CurrentPrice) }
				}
			});
		}

		public async Task<IEnumerable<Balance>> GetBalancesAsync(Account account) {
			await EnsureSessionAuthentication();

			var qBalances = await _client.GetBalances(_sessionState, account.ExternalId);

			return qBalances.Select(b => new Balance {
				Currency = ParseCurrency(b.Currency),
				Amount = Convert.ToDecimal(b.Cash)
			});
		}

		private Currency ParseCurrency(string currencyDtv) {
			if (Enum.TryParse<Currency>(currencyDtv, out var value)) {
				return value;
			}
			return Currency.Unknown;
		}

		private async Task EnsureSessionAuthentication()
		{
			if (_sessionState == null)
			{
				throw new Exception("QuestradeApi must be initialised.");
			}
			else if (!_sessionState.SessionValid())
			{
				_sessionState = await _client.Authenticate(_sessionState.RefreshToken);
			}
		}

		async Task<IEnumerable<ServiceAccount>> IService.GetAccountsAsync()
		{
			return (await GetAccountsAsync()).Select(a => new ServiceAccount {
				Account = a,
				Service = _serviceDefinition,
				GetBalancesAsync = () => GetBalancesAsync(a),
				GetPositionsAsync = () => GetPositionsAsync(a)
			});
		}

		Task<IEnumerable<Balance>> IService.GetBalancesAsync(ServiceAccount account)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Position>> IService.GetPositionsAsync(ServiceAccount account)
		{
			throw new NotImplementedException();
		}

		Task IService.Initialise(ServiceDefinition serviceDefinition)
		{
			_serviceDefinition = serviceDefinition;
			var refreshToken = _serviceInitialiser.GetRefreshToken($"{_serviceDefinition.Name}[{_serviceDefinition.Type}]");
			return Initialise(refreshToken);
		}
	}
}
