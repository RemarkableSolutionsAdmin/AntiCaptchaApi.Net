using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class RecaptchaV2ProxylessRequestTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new RecaptchaV2EnterpriseProxylessRequest()
            {
                WebsiteUrl = "http://http.myjino.ru/recaptcha/test-get.php",
                WebsiteKey = "6Lc_aCMTAAAAABx7u2W0WPXnVbI_v6ZdbM6rYf16"
            };

            
            TestCaptchaRequest(request, out TaskResultResponse<RecaptchaSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
    }
}