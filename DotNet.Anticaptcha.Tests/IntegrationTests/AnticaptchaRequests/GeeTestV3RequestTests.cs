using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class GeeTestV3RequestTests : GeeTestsBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var (gt, websiteChallenge) = GetTokens();
            var request = new GeeTestV3Request()
            {
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