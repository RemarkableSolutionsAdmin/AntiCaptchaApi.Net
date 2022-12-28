using AntiCaptchaApi.Net.Models.Solutions;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IRecaptchaV3Request : IWebCaptchaRequest<RecaptchaSolution>, IApiDomainArg, IIsEnterprise
{
    public string PageAction { get; set; }
    public double MinScore { get; set; }
}