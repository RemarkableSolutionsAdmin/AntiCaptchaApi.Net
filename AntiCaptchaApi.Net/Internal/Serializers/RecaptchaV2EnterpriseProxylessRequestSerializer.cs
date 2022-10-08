using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Serializers.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

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