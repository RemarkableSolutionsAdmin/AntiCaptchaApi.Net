using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed class RecaptchaV2EnterpriseRequestPayloadBuilder : RecaptchaV2EnterpriseProxylessRequestPayloadBuilder
{
    public override JObject Build(RecaptchaV2EnterpriseProxylessRequest request)
    {
        return base.Build(request)
            .With(((RecaptchaV2EnterpriseRequest)request).ProxyConfig)
            .WithUserAgent(((RecaptchaV2EnterpriseRequest)request).UserAgent)
            .WithCookies(((RecaptchaV2EnterpriseRequest)request).Cookies);
    }
}