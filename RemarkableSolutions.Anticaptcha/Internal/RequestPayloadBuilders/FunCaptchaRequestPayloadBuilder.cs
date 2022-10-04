using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed  class FunCaptchaRequestPayloadBuilder : FunCaptchaRequestProxylessPayloadBuilder
{
    public override string TypeName => "FunCaptchaTask";
    public override JObject Build(FunCaptchaRequestProxyless request)=> 
        base.Build(request)
            .With(((FunCaptchaRequest)request).ProxyConfig)
            .WithUserAgent(((FunCaptchaRequest)request).UserAgent);
}