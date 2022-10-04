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
    public class GeeTestProxylessRequestTests : AnticaptchaTestBase
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
            var request = new GeeTestV3ProxylessRequest()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = "http://www.supremenewyork.com",
                Gt = gt,
                Challenge = websiteChallenge
            };

            TestCaptchaRequest(request, out TaskResultResponse<GeeTestV3Solution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Challenge);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Validate);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.Seccode);
        }
    }
}