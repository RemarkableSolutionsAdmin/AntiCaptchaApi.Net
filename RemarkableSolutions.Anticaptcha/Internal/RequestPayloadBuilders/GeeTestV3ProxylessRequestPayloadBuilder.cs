using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal class GeeTestV3ProxylessRequestPayloadBuilder : CaptchaRequestPayloadBuilder<GeeTestV3ProxylessRequest>
{
    public override string TypeName => "GeeTestTaskProxyless";
    
    public override JObject Build(GeeTestV3ProxylessRequest request)
    {            
        var payload = base.Build(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("gt", request.Gt)
            .With("geetestGetLib", request.GeetestGetLib)
            .With("challenge", request.Challenge);
        if (!string.IsNullOrEmpty(request.GeetestApiServerSubdomain))
        {
            payload["geetestApiServerSubdomain"] = request.GeetestApiServerSubdomain;
        }

        return payload;
    }
}