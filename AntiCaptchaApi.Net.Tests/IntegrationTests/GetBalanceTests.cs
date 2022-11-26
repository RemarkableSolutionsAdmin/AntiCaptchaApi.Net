using System.Threading.Tasks;
using AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;
using AntiCaptchaApi.Net.Tests.IntegrationTests.Base;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests
{
    public class AnticaptchaRequestGetBalanceTests : AnticaptchaTestBase
    {
        [Fact]
        public async Task ShouldReturnCorrectBalance_WhenCallingAuthenticRequest()
        {
            var balance = await AnticaptchaClient.GetBalanceAsync();
            Assert.NotNull(balance);
            Assert.NotNull(balance.Balance);
        }
    }
}