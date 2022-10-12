using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal class GeeTestV4ProxylessRequestSerializer : CaptchaRequestSerializer<GeeTestV4ProxylessRequest, GeeTestV4Solution>
{
    public override string TypeName => "GeeTestTaskProxyless";
    public override JObject Serialize(GeeTestV4ProxylessRequest request)
    {            
        var payload = base.Serialize(request)
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