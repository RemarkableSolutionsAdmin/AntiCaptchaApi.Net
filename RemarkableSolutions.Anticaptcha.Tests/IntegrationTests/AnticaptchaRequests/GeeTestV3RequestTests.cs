using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class GeeTestV3RequestTests : GeeTestsBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var (gt, websiteChallenge) = GetTokens();
            var request = new GeeTestV3Request()
            {
                ClientKey = TestEnvironment.ClientKey,
                WebsiteUrl = "http://www.supremenewyork.com",
                Gt = gt,
                Challenge = websiteChallenge,
                UserAgent = TestEnvironment.UserAgent,
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            };

            TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
        }
    }
}