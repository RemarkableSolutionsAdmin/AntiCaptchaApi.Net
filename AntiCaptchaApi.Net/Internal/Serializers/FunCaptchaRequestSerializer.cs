using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed  class FunCaptchaRequestSerializer : FunCaptchaRequestProxylessSerializer
{
    public override string TypeName => "FunCaptchaTask";
    public override JObject Serialize(FunCaptchaRequestProxyless request)=> 
        base.Serialize(request)
            .With(((FunCaptchaRequest)request).ProxyConfig)
            .WithUserAgent(((FunCaptchaRequest)request).UserAgent);
}