using System.Threading.Tasks;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class FunCaptchaProxylessRequestTests : AnticaptchaTestBase
    {
        private const string FunCaptchaUriExample = "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";

        private static FunCaptchaProxylessRequest CreateAuthenticFunCaptchaRequest() =>
            new()
            {
                WebsiteUrl = FunCaptchaUriExample,
                WebsitePublicKey = "69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC",
                FunCaptchaApiJsSubdomain = "test",
                Data = "test",
            };

        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var captchaRequest = CreateAuthenticFunCaptchaRequest();
            TestCaptchaRequest(captchaRequest, out TaskResultResponse<FunCaptchaSolution> taskResult);
            Assert.NotNull(taskResult.Solution);
            Assert.NotNull(taskResult.Solution.Token);
        }

        [Fact]
        public  async Task ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequestAsync()
        {
            var captchaRequest = CreateAuthenticFunCaptchaRequest();
            await TestCaptchaRequestAsync(captchaRequest);
        }
    }
}
