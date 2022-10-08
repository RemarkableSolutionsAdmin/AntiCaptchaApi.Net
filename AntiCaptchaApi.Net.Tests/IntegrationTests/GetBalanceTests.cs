using System.Threading.Tasks;
using AntiCaptchaApi.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace AntiCaptchaApi.Tests.IntegrationTests
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