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
            WebsiteUrl = "http://www.supremenewyork.com",
            Gt = gt,
            Challenge = websiteChallenge
        };

        TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
    }
    string GetSiteKey(string pageSource)
    {
        var regex = new Regex("gt=(.*?)&");
        var gt = regex.Match(pageSource).Groups[1].Value;

        if (!string.IsNullOrEmpty(gt))
            return gt;
            
            
        regex = new Regex("captcha_id=(.*?)&");
        var captchaRegexGroups = regex.Match(pageSource).Groups;

        return captchaRegexGroups[1].Value;
    }

    private string GetChallenge(string pageSource)
    {
        var regex = new Regex("challenge=(.*?)&");
        return regex.Match(pageSource).Groups[1].Value;
    }
    [Fact]
    public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequestBySolveCaptcha()
    {
        var uri = "https://www.geetest.com/en/adaptive-captcha-demo"; 
        var response = new WebClient().DownloadString(uri);

        var request = new GeeTestV3ProxylessRequest()
        {
            WebsiteUrl = uri,
            Challenge = GetChallenge(response),
            Gt = GetSiteKey(response)
        };
        

        TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
        AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
    }
}