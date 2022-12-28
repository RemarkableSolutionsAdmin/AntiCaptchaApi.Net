namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IApiDomainArg : IRequestArg
{
    public string ApiDomain { get; set; }      
}