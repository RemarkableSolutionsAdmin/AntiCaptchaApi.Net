using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal sealed class RecaptchaV2RequestSerializer : RecaptchaV2ProxylessRequestSerializer
{        
    public override string TypeName => "RecaptchaV2Task";
    public override JObject Serialize(RecaptchaV2ProxylessRequest request)
    {
        return base.Serialize(request)
            .With(((RecaptchaV2Request)request).ProxyConfig)
            .WithUserAgent(((RecaptchaV2Request)request).UserAgent)
            .WithCookies(((RecaptchaV2Request)request).Cookies);
    }
}