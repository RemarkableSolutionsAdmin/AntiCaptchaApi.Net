using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class FunCaptchaRequestTests : AnticaptchaTestBase
    {
        private const string FunCaptchaUriExample = "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";

        private static FunCaptchaRequest CreateAuthenticFunCaptchaRequest() =>
            new()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = FunCaptchaUriExample,
                WebsitePublicKey = "69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116",
                FunCaptchaApiJsSubdomain = "test",
                Data = "test",
                ProxyConfig = TestHelper.GetCurrentTestProxyConfig()
            };

        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            if (!TestConfig.IsProxyDefined)
                Assert.True(TestConfig.IsProxyDefined);

            var captchaRequest = CreateAuthenticFunCaptchaRequest();
            TestCaptchaRequest(captchaRequest, out TaskResultResponse<FunCaptchaSolution> response);
            Assert.NotNull(response.Solution);
            Assert.NotNull(response.Solution.Token);
            Assert.NotEmpty(response.Solution.Token);
        }

        [Fact]
        public async Task ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequestAsync()
        {
            if(!TestConfig.IsProxyDefined)
                Assert.True(TestConfig.IsProxyDefined);

            var captchaRequest = CreateAuthenticFunCaptchaRequest();
            await TestCaptchaRequestAsync(captchaRequest);
        }
    }
}
