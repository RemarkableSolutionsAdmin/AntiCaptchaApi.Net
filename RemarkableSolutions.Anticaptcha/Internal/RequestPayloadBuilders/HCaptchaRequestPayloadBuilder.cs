using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed class HCaptchaRequestPayloadBuilder : HCaptchaProxylessRequestPayloadBuilder
{        
    public override string TypeName => "HCaptchaTask";
    public override JObject Build(HCaptchaProxylessRequest request)
    {
        return base.Build(request)
            .With(((HCaptchaRequest)request).ProxyConfig);
    }
}