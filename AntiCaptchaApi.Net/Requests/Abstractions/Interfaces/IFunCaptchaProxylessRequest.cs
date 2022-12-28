namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IFunCaptchaProxylessRequest : IWebsiteUrlArg
{
    public string WebsitePublicKey { get; set; }
    public string FunCaptchaApiJsSubdomain { get; set; }
    public string Data { get; set; }
}