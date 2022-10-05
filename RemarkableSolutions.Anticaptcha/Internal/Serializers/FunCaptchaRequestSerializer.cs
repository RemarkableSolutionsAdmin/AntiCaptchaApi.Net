using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal sealed  class FunCaptchaRequestSerializer : FunCaptchaRequestProxylessSerializer
{
    public override string TypeName => "FunCaptchaTask";
    public override JObject Serialize(FunCaptchaRequestProxyless request)=> 
        base.Serialize(request)
            .With(((FunCaptchaRequest)request).ProxyConfig)
            .WithUserAgent(((FunCaptchaRequest)request).UserAgent);
}