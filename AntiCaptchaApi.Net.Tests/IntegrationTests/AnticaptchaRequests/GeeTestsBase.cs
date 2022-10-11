﻿using System.Net;
using AntiCaptchaApi.Net.Tests.Models;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;

public abstract class GeeTestsBase : AnticaptchaTestBase
{
    protected static (string websiteKey, string websiteChallenge) GetTokens(string? url = null)
    {
        var response = new WebClient().DownloadString(url ?? "https://auth.geetest.com/api/init_captcha?time=1561554686474");
        var model = JsonConvert.DeserializeObject<GeeTestModel>(response);
        return (model.Data.Gt, model.Data.Challenge);
    }
}