using System.Threading.Tasks;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi.Requests;
using AntiCaptchaApi.Responses;
using AntiCaptchaApi.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Tests.IntegrationTests.AnticaptchaRequests
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