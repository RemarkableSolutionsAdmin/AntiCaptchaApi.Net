namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IWebsiteKeyArg : IRequestArg
{
    public string WebsiteKey { get; set; }
}