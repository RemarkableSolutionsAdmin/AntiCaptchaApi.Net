using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class HCaptchaProxylessRequestTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new HCaptchaProxylessRequest()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = "https://entwickler.ebay.de/signin?tab=register",
                WebsiteKey = "195eeb9f-8f50-4a9c-abfc-a78ceaa3cdde",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116"
            };
            
            TestCaptchaRequest(request, out TaskResultResponse<HCaptchaSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
    }
}