using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal class RecaptchaV2EnterpriseProxylessRequestPayloadBuilder: WebsiteCaptchaRequestPayloadBuilder<RecaptchaV2EnterpriseProxylessRequest>
{
    public override string TypeName => "RecaptchaV2EnterpriseTaskProxyless";
    public override JObject Build(RecaptchaV2EnterpriseProxylessRequest request)
    {            
        var payload = base.Build(request)
            .With("apiDomain", request.ApiDomain);
        if (request.EnterprisePayload.Count > 0)
        {
            payload["enterprisePayload"] = JObject.FromObject(request.EnterprisePayload);
        }

        return payload;
    }
}