using PortfolioSmarts.Domain.Contract.Portfolio;
using System;
using Xunit;

namespace Domain.Contract.UnitTest
{
	public class SecurityTests
	{
		[Fact]
		public void Symbol_Always_Upper_Case()
		{
			var sut = new Security {
				Symbol = "lowercase_symbol"
			};

			Assert.Equal("LOWERCASE_SYMBOL", sut.Symbol);
		}

		[Fact]
		public void Exchange_Always_Upper_Case()
		{
			var sut = new Security {
				Exchange = "lowercase_exchange"
			};

			Assert.Equal("LOWERCASE_EXCHANGE", sut.Exchange);
		}
	}
}
