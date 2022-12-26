using System.Collections.Generic;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.IntegrationTests.Base;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class AntiGateCaptchaRequestTests : AnticaptchaRequestTestBase <AntiGateSolution>
    {
        private const string UriExample = "https://anti-captcha.com/tutorials/v2-textarea";

        protected override AntiGateRequest CreateAuthenticRequest() =>
            new()
            {
                WebsiteUrl = UriExample,
                TemplateName = "CloudFlare cookies for a proxy",
                Variables = new JObject(),
                DomainsOfInterest = new List<string>
                {
                  "anything"  
                },
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            };

        protected override void AssertTaskResult(TaskResultResponse<AntiGateSolution> taskResult)
        {
            Assert.NotNull(taskResult.Solution);
            Assert.NotNull(taskResult.Solution.Cookies);
            Assert.NotNull(taskResult.Solution.LocalStorage);
            Assert.NotNull(taskResult.Solution.Fingerprint);
            Assert.NotEmpty(taskResult.Solution.Url);
            Assert.NotEmpty(taskResult.Solution.Domain);
        }


        [Fact]
        public async Task ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            await TestAuthenticRequest();
        }
    }
}
