using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

internal sealed  class FunCaptchaRequestSerializer : FunCaptchaRequestProxylessSerializer
{
    public override string TypeName => "FunCaptchaTask";
    public override JObject Serialize(FunCaptchaRequestProxyless request)=> 
        base.Serialize(request)
            .With(((FunCaptchaRequest)request).ProxyConfig)
            .WithUserAgent(((FunCaptchaRequest)request).UserAgent);
}