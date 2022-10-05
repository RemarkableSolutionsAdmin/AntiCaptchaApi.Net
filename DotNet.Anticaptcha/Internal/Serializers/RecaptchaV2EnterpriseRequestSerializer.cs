using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

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