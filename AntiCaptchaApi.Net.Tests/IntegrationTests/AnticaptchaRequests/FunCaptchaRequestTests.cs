using System.Threading.Tasks;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class FunCaptchaRequestTests : AnticaptchaTestBase
    {
        private const string FunCaptchaUriExample = "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";

        private static FunCaptchaRequest CreateAuthenticFunCaptchaRequest() =>
            new()
            {
                WebsiteUrl = FunCaptchaUriExample,
                WebsitePublicKey = "69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC",
                UserAgent = TestEnvironment.UserAgent,
                FunCaptchaApiJsSubdomain = "test",
                Data = "test",
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            };

        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            if (!TestEnvironment.IsProxyDefined)
                Assert.True(TestEnvironment.IsProxyDefined);

            var captchaRequest = CreateAuthenticFunCaptchaRequest();
            TestCaptchaRequest(captchaRequest, out TaskResultResponse<FunCaptchaSolution> response);
            Assert.NotNull(response.Solution);
            Assert.NotNull(response.Solution.Token);
            Assert.NotEmpty(response.Solution.Token);
        }

        [Fact]
        public async Task ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequestAsync()
        {
            if(!TestEnvironment.IsProxyDefined)
                Assert.True(TestEnvironment.IsProxyDefined);

            var captchaRequest = CreateAuthenticFunCaptchaRequest();
            await TestCaptchaRequestAsync(captchaRequest);
        }
    }
}
