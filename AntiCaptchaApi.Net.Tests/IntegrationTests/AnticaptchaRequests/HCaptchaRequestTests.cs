using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class HCaptchaRequestTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new HCaptchaRequest()
            {
                WebsiteUrl = "https://democaptcha.com/demo-form-eng/hcaptcha.html/",
                WebsiteKey = "51829642-2cda-4b09-896c-594f89d700cc",
                UserAgent = TestEnvironment.UserAgent,
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            };
            
            TestCaptchaRequest(request, out TaskResultResponse<HCaptchaSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
    }
}