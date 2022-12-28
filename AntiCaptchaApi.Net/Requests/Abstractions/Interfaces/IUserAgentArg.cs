namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IUserAgentArg : IRequestArg
{
    public string UserAgent { get; set; }
}