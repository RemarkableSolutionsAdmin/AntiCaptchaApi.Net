using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed class HCaptchaRequestSerializer : HCaptchaProxylessRequestSerializer
{        
    public override string TypeName => "HCaptchaTask";
    public override JObject Serialize(HCaptchaProxylessRequest request)
    {
        return base.Serialize(request)
            .With(((HCaptchaRequest)request).ProxyConfig);
    }
}