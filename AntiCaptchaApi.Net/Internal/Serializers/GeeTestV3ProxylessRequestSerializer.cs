using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal class GeeTestV3ProxylessRequestSerializer : CaptchaRequestSerializer<GeeTestV3ProxylessRequest, GeeTestV3Solution>
{
    public override string TypeName => "GeeTestTaskProxyless";
    
    public override JObject Serialize(GeeTestV3ProxylessRequest request)
    {            
        var payload = base.Serialize(request)
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