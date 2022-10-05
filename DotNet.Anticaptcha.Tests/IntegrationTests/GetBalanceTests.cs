using System.Threading.Tasks;
using DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests
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