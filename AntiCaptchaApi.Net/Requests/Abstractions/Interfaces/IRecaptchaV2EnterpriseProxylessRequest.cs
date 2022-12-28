using AntiCaptchaApi.Net.Models.Solutions;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IRecaptchaV2EnterpriseProxylessRequest : IEnterprisePayloadArg, IWebCaptchaRequest<RecaptchaSolution>, IApiDomainArg
{
    
}