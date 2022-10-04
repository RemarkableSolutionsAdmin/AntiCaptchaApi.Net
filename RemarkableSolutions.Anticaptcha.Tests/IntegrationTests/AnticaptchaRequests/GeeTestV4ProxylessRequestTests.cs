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
    public class GeeTestV4ProxylessRequestTests : AnticaptchaTestBase
    {
        private class GeeTestModel
        {
            public GeeTestData Data { get; set; }
            public string Status { get; set; }
            [JsonProperty("err_msg")] public string ErrorMessage { get; set; }
        }

        private class GeeTestData
        {
            [JsonProperty("gt")] public string WebsiteKey { get; set; }
            [JsonProperty("challenge")] public string WebsiteChallenge { get; set; }
        }

        private (string websiteKey, string websiteChallenge) GetTokens()
        {
            var response = new WebClient().DownloadString("https://auth.geetest.com/api/init_captcha?time=1561554686474");
            var model = JsonConvert.DeserializeObject<GeeTestModel>(response);
            return (model.Data.WebsiteKey, model.Data.WebsiteChallenge);
        }

        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            //unsolvable captcha 
            //TODO: Find a proper one

            var (websiteKey, websiteChallenge) = GetTokens();
            var request = new GeeTestV4ProxylessRequest()
            {
                ClientKey = TestConfig.ClientKey,
                WebsiteUrl = "http://www.supremenewyork.com",
                Gt = websiteKey,
                Challenge = websiteChallenge
            };

            request.InitParameters.Add("riskType", "slide");

            TestCaptchaRequest(request, out TaskResultResponse<GeeTestV4Solution> taskResultResponse);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.CaptchaId);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.LotNumber);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.PassToken);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.GenTime);
            AssertHelper.NotNullNotEmpty(taskResultResponse.Solution.CaptchaOutput);
            
        }
    }
}