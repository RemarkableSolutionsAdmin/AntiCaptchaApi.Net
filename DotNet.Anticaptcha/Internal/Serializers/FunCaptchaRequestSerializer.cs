using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

internal sealed  class FunCaptchaRequestSerializer : FunCaptchaRequestProxylessSerializer
{
    public override string TypeName => "FunCaptchaTask";
    public override JObject Serialize(FunCaptchaRequestProxyless request)=> 
        base.Serialize(request)
            .With(((FunCaptchaRequest)request).ProxyConfig)
            .WithUserAgent(((FunCaptchaRequest)request).UserAgent);
}