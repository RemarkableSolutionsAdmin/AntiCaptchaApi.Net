namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IWebsiteUrlArg : IRequestArg
{
    public string WebsiteUrl { get; set; }
}