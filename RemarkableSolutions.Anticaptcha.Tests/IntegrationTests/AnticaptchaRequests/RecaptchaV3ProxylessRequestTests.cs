using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class RecaptchaV3ProxylessRequestTests : AnticaptchaTestBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new RecaptchaV3ProxylessRequest()
            {
                WebsiteUrl = "https://www.netflix.com/login",
                WebsiteKey = "6Lf8hrcUAAAAAIpQAFW2VFjtiYnThOjZOA5xvLyR",
                IsEnterprise = true
            };

            
            TestCaptchaRequest(request, out TaskResultResponse<RecaptchaSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
        
        [Fact]
        public void ShouldReturnCorrectRawCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = new RecaptchaV3ProxylessRequest()
            {
                WebsiteUrl = "https://www.netflix.com/login",
                WebsiteKey = "6Lf8hrcUAAAAAIpQAFW2VFjtiYnThOjZOA5xvLyR",
                IsEnterprise = true
            };

            
            TestCaptchaRequest(request, out TaskResultResponse<RawSolution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GRecaptchaResponse);
        }
    }
}