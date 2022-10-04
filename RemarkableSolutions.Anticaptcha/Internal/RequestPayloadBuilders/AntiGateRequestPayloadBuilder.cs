using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Enums;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed  class AntiGateRequestPayloadBuilder : CaptchaRequestPayloadBuilder<AntiGateRequest>
{
    public override string TypeName => "AntiGateTask";
    public override JObject Build(AntiGateRequest request)
    {
        if (request.ProxyConfig != null)
            request.ProxyConfig.ProxyType = ProxyTypeOption.Http;
        
        var payload = base.Build(request)
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