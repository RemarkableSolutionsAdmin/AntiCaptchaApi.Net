using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed  class FunCaptchaRequestSerializer : FunCaptchaProxylessRequestSerializer
{
    public override string TypeName => "FunCaptchaTask";
    public override JObject Serialize(FunCaptchaProxylessRequest request)=> 
        base.Serialize(request)
            .With(((FunCaptchaRequest)request).ProxyConfig)
            .WithUserAgent(((FunCaptchaRequest)request).UserAgent);
}