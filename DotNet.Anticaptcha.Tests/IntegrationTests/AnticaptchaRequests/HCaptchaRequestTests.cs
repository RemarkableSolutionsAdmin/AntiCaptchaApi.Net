using System.Threading.Tasks;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
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