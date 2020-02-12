using PortfolioSmarts.Domain;
using System;
using Xunit;

namespace Domain.UnitTest
{
    public class TransactionTests
    {
        [Fact]
        public void Transaction_Valid()
        {
            var account = new Account();
            var security = new Security();
            var sut = new Transaction {
                Account = account,
                Date = DateTime.Now,
                Security = security,
                Price = 12.34M,
                Shares = 567M
            };

            Assert.True(sut.Valid());
        }
    }
}
