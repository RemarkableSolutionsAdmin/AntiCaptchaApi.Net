using System.Threading.Tasks;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using AntiCaptchaApi.Net.Tests.IntegrationTests.Base;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;

public class HCaptchaRequestTests : AnticaptchaRequestTestBase<HCaptchaSolution>
{
    [Fact]
    public async Task ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
    {
        await TestAuthenticRequest();
    }

    protected override HCaptchaRequest CreateAuthenticRequest()
    {
        return new HCaptchaRequest()
        {
            WebsiteUrl = "https://democaptcha.com/demo-form-eng/hcaptcha.html/",
            WebsiteKey = "51829642-2cda-4b09-896c-594f89d700cc",
            UserAgent = TestEnvironment.UserAgent,
            ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
        };
    }

    protected override void AssertTaskResult(TaskResultResponse<HCaptchaSolution> taskResult)
    {
        AssertHelper.NotNullNotEmpty(taskResult.Solution.GRecaptchaResponse);
    }
}