using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class RecaptchaV2ProxylessRequestTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new RecaptchaV2EnterpriseProxylessRequest()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = "http://http.myjino.ru/recaptcha/test-get.php",
                WebsiteKey = "6Lc_aCMTAAAAABx7u2W0WPXnVbI_v6ZdbM6rYf16"
            };

            
            TestCaptchaRequest(request, out TaskResultResponse<RecaptchaSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
    }
}