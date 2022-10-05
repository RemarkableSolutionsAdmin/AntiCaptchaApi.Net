using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal sealed class HCaptchaRequestSerializer : HCaptchaProxylessRequestSerializer
{        
    public override string TypeName => "HCaptchaTask";
    public override JObject Serialize(HCaptchaProxylessRequest request)
    {
        return base.Serialize(request)
            .With(((HCaptchaRequest)request).ProxyConfig);
    }
}