using AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests
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