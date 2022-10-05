using System.Threading.Tasks;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
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
            
            TestCaptchaRequest<RecaptchaV2EnterpriseProxylessRequest, RawSolution>(request);
        }
        
        
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

            var result = AnticaptchaClient.SolveCaptchaRaw<RecaptchaV2EnterpriseProxylessRequest, RecaptchaSolution>(request);
            Assert.NotNull(result);
            Assert.True(result.Solution.CreateTaskResponse.HasNoErrors);
            Assert.NotNull(result.Solution.CreateTaskResponse);
            Assert.Null(result.Solution.CreateTaskResponse.ErrorCode);
            AssertHelper.NotNullNotEmpty(result.Solution.GRecaptchaResponse);
        }
    }
}