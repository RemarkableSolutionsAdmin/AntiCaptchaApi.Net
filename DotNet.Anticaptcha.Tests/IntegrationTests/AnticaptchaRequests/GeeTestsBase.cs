using System.Net;
using Newtonsoft.Json;
using DotNet.Anticaptcha.Tests.Models;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;

public abstract class GeeTestsBase : AnticaptchaTestBase
{
    protected static (string websiteKey, string websiteChallenge) GetTokens(string? url = null)
    {
        var response = new WebClient().DownloadString(url ?? "https://auth.geetest.com/api/init_captcha?time=1561554686474");
        var model = JsonConvert.DeserializeObject<GeeTestModel>(response);
        return (model.Data.Gt, model.Data.Challenge);
    }
}