using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;

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
    [Fact]
    public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequestBySolveCaptcha()
    {
        var (gt, websiteChallenge) = GetTokens();
        var request = new GeeTestV3ProxylessRequest()
        {
            WebsiteUrl = "https://www.geetest.com/en/adaptive-captcha-demo",
            Gt = gt,
            Challenge = websiteChallenge
        };

        TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
    }
}