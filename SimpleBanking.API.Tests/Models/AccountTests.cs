using SimpleBanking.API.Models;

namespace SimpleBanking.API.Tests.Models
{
    public class AccountTests
    {
        [Fact]
        public void Account_ShouldHaveAnId()
        {
            var account = new Account();

            account.Id = "12345";

            Assert.NotNull(account.Id);
            Assert.Equal("12345", account.Id);
        }

        [Fact]
        public void Account_ShouldHaveInitialBalance()
        {
            var account = new Account();

            account.Balance = 100;

            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Account_ShouldUpdateBalanceCorrectly()
        {
            var account = new Account { Balance = 100 };

            account.Balance += 50;

            Assert.Equal(150, account.Balance);
        }

        [Fact]
        public void Account_ShouldHandleNegativeBalance()
        {
            var account = new Account { Balance = 100 };

            account.Balance -= 150;

            Assert.Equal(-50, account.Balance);
        }
    }
}