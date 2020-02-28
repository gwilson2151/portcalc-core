using System;

namespace PortfolioSmarts.Questrade
{
	public class ConsoleQuestradeInitialiser
	{
		public string GetRefreshToken(string serviceIdentifier)
		{
			Console.Write($"Enter refresh token for {serviceIdentifier}: ");
			var refreshToken = Console.ReadLine();
			// todo - ensure non-null, non-empty, non-whitespace token
			return refreshToken;
		}
	}
}