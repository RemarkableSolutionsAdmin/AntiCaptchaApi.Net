using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Serializers.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal class RecaptchaV2EnterpriseProxylessRequestSerializer: WebsiteCaptchaRequestSerializer<RecaptchaV2EnterpriseProxylessRequest>
{
    public override string TypeName => "RecaptchaV2EnterpriseTaskProxyless";
    public override JObject Serialize(RecaptchaV2EnterpriseProxylessRequest request)
    {            
        var payload = base.Serialize(request)
            .With("apiDomain", request.ApiDomain);
        if (request.EnterprisePayload.Count > 0)
        {
            payload["enterprisePayload"] = JObject.FromObject(request.EnterprisePayload);
        }

        return payload;
    }
}