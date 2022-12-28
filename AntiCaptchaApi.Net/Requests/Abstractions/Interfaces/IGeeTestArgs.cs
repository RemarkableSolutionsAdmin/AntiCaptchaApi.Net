namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IGeeTestArgs : IWebsiteUrlArg, IRequestArg
{
    public string Gt { get; set; }
    public string Challenge { get; set; }
    public string GeetestApiServerSubdomain { get; set; }
    public string GeetestGetLib { get; set; }
}