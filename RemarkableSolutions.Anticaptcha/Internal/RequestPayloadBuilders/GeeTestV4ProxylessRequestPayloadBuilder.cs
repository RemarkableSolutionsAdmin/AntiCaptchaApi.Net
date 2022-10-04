using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal class GeeTestV4ProxylessRequestPayloadBuilder : CaptchaRequestPayloadBuilder<GeeTestV4ProxylessRequest>
{
    public override string TypeName => "GeeTestTaskProxyless";
    public override JObject Build(GeeTestV4ProxylessRequest request)
    {            
        var payload = base.Build(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("gt", request.Gt)
            .With("challenge", request.Challenge)
            .With("geetestGetLib", request.GeetestGetLib)
            .With("version", 4);

        if (!string.IsNullOrEmpty(request.GeetestApiServerSubdomain))
        {
            payload["geetestApiServerSubdomain"] = request.GeetestApiServerSubdomain;
        }
        if (request.InitParameters != null && request.InitParameters.Count > 0)
        {
            payload["initParameters"] = JObject.FromObject(request.InitParameters);
        }

        return payload;
    }
}