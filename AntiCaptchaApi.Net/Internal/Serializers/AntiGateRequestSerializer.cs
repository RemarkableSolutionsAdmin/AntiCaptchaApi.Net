using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Enums;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Serializers.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

internal sealed  class AntiGateRequestSerializer : CaptchaRequestSerializer<AntiGateRequest>
{
    public override string TypeName => "AntiGateTask";
    public override JObject Serialize(AntiGateRequest request)
    {
        if (request.ProxyConfig != null)
            request.ProxyConfig.ProxyType = ProxyTypeOption.Http;
        
        var payload = base.Serialize(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("templateName", request.TemplateName)
            .WithIf(request.ProxyConfig, !string.IsNullOrEmpty(request.ProxyConfig.ProxyAddress));
            
        if (request.Variables != null)
        {
            payload["variables"] = request.Variables;
        }

        if (request.DomainsOfInterest != null && request.DomainsOfInterest.Count > 0)
        {
            payload["domainsOfInterest"] = JToken.FromObject(request.DomainsOfInterest);
        }

        return payload;
    }
}