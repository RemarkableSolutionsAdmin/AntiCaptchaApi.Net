using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests
{
    public class AnticaptchaGetBalanceTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectBalance_WhenCallingAuthenticRequest()
        {
            var balance = AnticaptchaClient.GetBalance();
            Assert.NotNull(balance);
            Assert.NotNull(balance.Balance);
        }
    }
}