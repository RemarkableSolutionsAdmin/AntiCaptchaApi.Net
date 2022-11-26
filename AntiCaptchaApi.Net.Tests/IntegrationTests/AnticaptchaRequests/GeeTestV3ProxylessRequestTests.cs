using System;
using System.Net;
using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;

public class GeeTestProxylessV3RequestTests : GeeTestsBase
{
    [Fact]
    public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
    {
        var (gt, websiteChallenge) = GetTokens();
        var request = new GeeTestV3ProxylessRequest()
        {
            WebsiteUrl = "https://www.seloger.com/",
            Gt = "1e505deed3832c02c96ca5abe70df9ab",
            Challenge = "1ea0aba2104e7366ba764f86527df09a"
        };

        TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
    }

    // [Fact]
    // public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequestBySolveCaptcha()
    // {
    //     var (gt, websiteChallenge) = GetTokens();
    //     var request = new GeeTestV3ProxylessRequest()
    //     {
    //         WebsiteUrl = "http://www.supremenewyork.com",
    //         Gt = gt,
    //         Challenge = websiteChallenge
    //     };
    //     
    //
    //     TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
    //     AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
    //     AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
    //     AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
    // }
}