using AntiCaptchaApi.Net.Models.Solutions;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IHCaptchaProxylessRequest : IWebCaptchaRequest<HCaptchaSolution>, IUserAgentArg, IIsInvisibleArg, IEnterprisePayloadArg
{
    
}