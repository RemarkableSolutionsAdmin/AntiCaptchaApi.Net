using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class GeeTestV3RequestTests : AnticaptchaTestBase
    {
        private class GeeTestModel
        {
            public GeeTestData Data { get; set; }
            public string Status { get; set; }
            [JsonProperty("err_msg")]
            public string ErrorMessage { get; set; }
        }

        private class GeeTestData
        {
            [JsonProperty("gt")]
            public string Gt { get; set; }
            [JsonProperty("challenge")]
            public string WebsiteChallenge { get; set; }
        }
        
        private (string websiteKey, string websiteChallenge) GetTokens()
        {
            var response = new WebClient().DownloadString("https://auth.geetest.com/api/init_captcha?time=1561554686474");
            var model = JsonConvert.DeserializeObject<GeeTestModel>(response);
            return (model.Data.Gt, model.Data.WebsiteChallenge);
        }
        
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var (gt, websiteChallenge) = GetTokens();
            var request = new GeeTestV3Request()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = "http://www.supremenewyork.com",
                Gt = gt,
                Challenge = websiteChallenge,
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116",
                ProxyConfig = TestHelper.GetCurrentTestProxyConfig()
            };

            TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
        }
    }
}