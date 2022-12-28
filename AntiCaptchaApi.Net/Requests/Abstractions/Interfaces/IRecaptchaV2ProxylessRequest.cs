namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IRecaptchaV2ProxylessRequest : IWebCaptchaRequest, IIsInvisibleArg
{
    public string RecaptchaDataSValue { get; set; }
}