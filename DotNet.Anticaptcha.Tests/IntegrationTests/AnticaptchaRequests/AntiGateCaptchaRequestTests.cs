﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Responses;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests
{
    public class AntiGateCaptchaRequestTests : AnticaptchaTestBase
    {
        private const string UriExample = "https://anti-captcha.com/tutorials/v2-textarea";

        private static AntiGateRequest CreateAuthenticRequest() =>
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

        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            if (!TestEnvironment.IsProxyDefined)
                Assert.True(TestEnvironment.IsProxyDefined);

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