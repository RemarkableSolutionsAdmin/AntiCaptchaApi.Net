using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed class RecaptchaV2RequestPayloadBuilder : RecaptchaV2ProxylessRequestPayloadBuilder
{        
    public override string TypeName => "RecaptchaV2Task";
    public override JObject Build(RecaptchaV2ProxylessRequest request)
    {
        return base.Build(request)
            .With(((RecaptchaV2Request)request).ProxyConfig)
            .WithUserAgent(((RecaptchaV2Request)request).UserAgent)
            .WithCookies(((RecaptchaV2Request)request).Cookies);
    }
}