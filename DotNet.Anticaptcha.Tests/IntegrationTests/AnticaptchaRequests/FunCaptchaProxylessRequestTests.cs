using System.Threading.Tasks;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class FunCaptchaProxylessRequestTests : AnticaptchaTestBase
    {
        private const string FunCaptchaUriExample = "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";

        private static FunCaptchaRequestProxyless CreateAuthenticFunCaptchaRequest() =>
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
