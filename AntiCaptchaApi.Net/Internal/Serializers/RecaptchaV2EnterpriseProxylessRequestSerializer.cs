using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal class RecaptchaV2EnterpriseProxylessRequestSerializer: WebsiteCaptchaRequestSerializer<RecaptchaV2EnterpriseProxylessRequest, RecaptchaSolution>
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