using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

internal sealed class HCaptchaRequestSerializer : HCaptchaProxylessRequestSerializer
{        
    public override string TypeName => "HCaptchaTask";
    public override JObject Serialize(HCaptchaProxylessRequest request)
    {
        return base.Serialize(request)
            .With(((HCaptchaRequest)request).ProxyConfig);
    }
}