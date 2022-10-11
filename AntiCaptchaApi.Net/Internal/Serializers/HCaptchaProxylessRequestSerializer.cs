using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal class HCaptchaProxylessRequestSerializer : WebsiteCaptchaRequestSerializer<HCaptchaProxylessRequest>
{
    public override string TypeName => "HCaptchaTaskProxyless";
    public override JObject Serialize(HCaptchaProxylessRequest request)
    {            
        var payload = base.Serialize(request)
            .WithUserAgent(request.UserAgent)
            .With("isInvisible", request.IsInvisible);

        if (request.EnterprisePayload.Count > 0)
        {
            payload["enterprisePayload"] = JObject.FromObject(request.EnterprisePayload);
        }

        return payload;
    }
}