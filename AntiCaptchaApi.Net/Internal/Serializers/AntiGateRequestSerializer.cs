using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed  class AntiGateRequestSerializer : CaptchaRequestSerializer<AntiGateRequest, AntiGateSolution>
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