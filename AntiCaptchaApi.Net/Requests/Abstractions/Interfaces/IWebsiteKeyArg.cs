namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IWebsiteKeyArg : IRequestArg
{
    public string WebsiteUrl { get; set; }
}