using System.Threading.Tasks;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using AntiCaptchaApi.Net.Tests.IntegrationTests.Base;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;

public class HCaptchaProxylessRequestTests : AnticaptchaRequestTestBase<HCaptchaSolution>
{
    [Fact]
    public async Task ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
    {
        await TestAuthenticRequest();
    }

    protected override HCaptchaProxylessRequest CreateAuthenticRequest()
    {
        return new HCaptchaProxylessRequest()
        {
            WebsiteUrl = "https://entwickler.ebay.de/signin?tab=register",
            WebsiteKey = "195eeb9f-8f50-4a9c-abfc-a78ceaa3cdde",
            UserAgent = TestEnvironment.UserAgent
        };
    }

    protected override void AssertTaskResult(TaskResultResponse<HCaptchaSolution> taskResult)
    {
        AssertHelper.NotNullNotEmpty(taskResult.Solution.GRecaptchaResponse);
    }
}