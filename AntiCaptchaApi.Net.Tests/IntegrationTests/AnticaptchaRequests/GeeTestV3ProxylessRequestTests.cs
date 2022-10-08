using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi.Requests;
using AntiCaptchaApi.Responses;
using AntiCaptchaApi.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Tests.IntegrationTests.AnticaptchaRequests
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