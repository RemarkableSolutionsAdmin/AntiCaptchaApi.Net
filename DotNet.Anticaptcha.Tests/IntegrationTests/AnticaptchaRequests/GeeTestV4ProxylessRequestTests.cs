using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class GeeTestV4ProxylessRequestTests : GeeTestsBase
    {
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            //unsolvable captcha 
            //TODO: Find a proper one

            var (websiteKey, websiteChallenge) = GetTokens();
            var request = new GeeTestV4ProxylessRequest()
            {
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