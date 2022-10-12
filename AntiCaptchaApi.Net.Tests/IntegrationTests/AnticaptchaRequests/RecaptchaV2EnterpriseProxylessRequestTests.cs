using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class RecaptchaV2EnterpriseProxylessRequestTests : AnticaptchaTestBase
    {
         [Fact]
         public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
         {
             var request = new RecaptchaV2EnterpriseProxylessRequest
             {
                 WebsiteUrl = "https://store.steampowered.com/join",
                 WebsiteKey = "6LdIFr0ZAAAAAO3vz0O0OQrtAefzdJcWQM2TMYQH"
             };
        
             request.EnterprisePayload.Add("test", "qwerty");
             request.EnterprisePayload.Add("secret", "AB_12345");
             
             TestCaptchaRequest(request);
         }
        
        //
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingFactualAnticaptchaSolve()
        {
            var request = new RecaptchaV2EnterpriseProxylessRequest
            {
                WebsiteUrl = "https://store.steampowered.com/join",
                WebsiteKey = "6LdIFr0ZAAAAAO3vz0O0OQrtAefzdJcWQM2TMYQH"
            };

            request.EnterprisePayload.Add("test", "qwerty");
            request.EnterprisePayload.Add("secret", "AB_12345");

            var result = AnticaptchaClient.SolveCaptcha(request);
            Assert.NotNull(result);
            Assert.False(result.CreateTaskResponse.IsErrorResponse);
            Assert.NotNull(result.CreateTaskResponse);
            Assert.Null(result.CreateTaskResponse.ErrorCode);
            AssertHelper.NotNullNotEmpty(result.Solution.GRecaptchaResponse);
        }
    }
}