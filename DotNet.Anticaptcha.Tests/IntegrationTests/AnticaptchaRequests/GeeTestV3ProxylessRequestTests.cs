using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class GeeTestProxylessRequestTests : GeeTestsBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var (gt, websiteChallenge) = GetTokens();
            var request = new GeeTestV3ProxylessRequest()
            {
                WebsiteUrl = "http://www.supremenewyork.com",
                Gt = gt,
                Challenge = websiteChallenge
            };

            TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
        }
    }
}