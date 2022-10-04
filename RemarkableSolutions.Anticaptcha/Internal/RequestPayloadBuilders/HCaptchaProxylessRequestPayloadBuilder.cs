using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal class HCaptchaProxylessRequestPayloadBuilder : WebsiteCaptchaRequestPayloadBuilder<HCaptchaProxylessRequest>
{
    public override string TypeName => "HCaptchaTaskProxyless";
    public override JObject Build(HCaptchaProxylessRequest request)
    {            
        var payload = base.Build(request)
            .WithUserAgent(request.UserAgent)
            .With("isInvisible", request.IsInvisible);

        if (request.EnterprisePayload.Count > 0)
        {
            payload["enterprisePayload"] = JObject.FromObject(request.EnterprisePayload);
        }

        return payload;
    }
}