using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class AntiGateCaptchaRequestTests : AnticaptchaTestBase
    {
        private const string UriExample = "https://anti-captcha.com/tutorials/v2-textarea";

        private static AntiGateRequest CreateAuthenticRequest() =>
            new()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = UriExample,
                TemplateName = "CloudFlare cookies for a proxy",
                Variables = new JObject(),
                DomainsOfInterest = new List<string>
                {
                  "dupa"  
                },
                ProxyConfig = TestHelper.GetCurrentTestProxyConfig()
            };

        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            if (!TestConfig.IsProxyDefined)
                Assert.True(TestConfig.IsProxyDefined);

            var captchaRequest = CreateAuthenticRequest();
            TestCaptchaRequest(captchaRequest, out TaskResultResponse<AntiGateSolution> taskResult);
            Assert.NotNull(taskResult.Solution);
            Assert.NotNull(taskResult.Solution.Cookies);
            Assert.NotNull(taskResult.Solution.LocalStorage);
            Assert.NotNull(taskResult.Solution.Fingerprint);
            Assert.NotEmpty(taskResult.Solution.Url);
            Assert.NotEmpty(taskResult.Solution.Domain);
        }
    }
}
