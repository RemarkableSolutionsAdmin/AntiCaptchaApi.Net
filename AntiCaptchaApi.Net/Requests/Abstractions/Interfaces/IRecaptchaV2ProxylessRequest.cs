using AntiCaptchaApi.Net.Models.Solutions;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IRecaptchaV2ProxylessRequest : IWebCaptchaRequest<RecaptchaSolution>, IIsInvisibleArg
{
    public string RecaptchaDataSValue { get; set; }
}