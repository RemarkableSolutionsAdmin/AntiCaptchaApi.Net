using System.Threading.Tasks;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests
{
    public class AnticaptchaGetBalanceTests
    {
        [Fact]
        public void ShouldReturnCorrectBalance_WhenCallingAuthenticRequest()
        {
            var balance = AnticaptchaManager.GetBalance(clientKey: TestEnvironment.ClientKey);
            Assert.NotNull(balance);
            Assert.NotNull(balance.Balance);
        }
    }
}