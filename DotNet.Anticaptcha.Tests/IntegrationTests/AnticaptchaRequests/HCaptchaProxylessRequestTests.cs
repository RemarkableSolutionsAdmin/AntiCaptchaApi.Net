using System.Threading.Tasks;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class HCaptchaProxylessRequestTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new HCaptchaProxylessRequest()
            {
                WebsiteUrl = "https://entwickler.ebay.de/signin?tab=register",
                WebsiteKey = "195eeb9f-8f50-4a9c-abfc-a78ceaa3cdde",
                UserAgent = TestEnvironment.UserAgent
            };
            
            TestCaptchaRequest(request, out TaskResultResponse<HCaptchaSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
    }
}