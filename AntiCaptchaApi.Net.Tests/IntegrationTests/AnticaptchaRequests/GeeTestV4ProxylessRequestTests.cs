using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class GeeTestV4ProxylessRequestTests : GeeTestsBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            //unsolvable captcha 
            //TODO: Find a proper one

            var (websiteKey, websiteChallenge) = GetTokens();
            var request = new GeeTestV4ProxylessRequest()
            {
                WebsiteUrl = "http://www.supremenewyork.com",
                Gt = websiteKey,
                Challenge = websiteChallenge
            };

            request.InitParameters.Add("riskType", "slide");

            TestCaptchaRequest(request, out TaskResultResponse<GeeTestV4Solution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.CaptchaId);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.LotNumber);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.PassToken);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GenTime);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.CaptchaOutput);
            
        }
    }
}