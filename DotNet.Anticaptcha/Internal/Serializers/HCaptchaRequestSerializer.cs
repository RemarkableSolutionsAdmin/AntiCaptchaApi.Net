using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

internal sealed class HCaptchaRequestSerializer : HCaptchaProxylessRequestSerializer
{        
    public override string TypeName => "HCaptchaTask";
    public override JObject Serialize(HCaptchaProxylessRequest request)
    {
        return base.Serialize(request)
            .With(((HCaptchaRequest)request).ProxyConfig);
    }
}