using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

internal sealed class RecaptchaV2EnterpriseRequestSerializer : RecaptchaV2EnterpriseProxylessRequestSerializer
{
    public override JObject Serialize(RecaptchaV2EnterpriseProxylessRequest request)
    {
        return base.Serialize(request)
            .With(((RecaptchaV2EnterpriseRequest)request).ProxyConfig)
            .WithUserAgent(((RecaptchaV2EnterpriseRequest)request).UserAgent)
            .WithCookies(((RecaptchaV2EnterpriseRequest)request).Cookies);
    }
}