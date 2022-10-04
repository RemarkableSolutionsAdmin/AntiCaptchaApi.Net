using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed class GeeTestV4RequestPayloadBuilder : GeeTestV4ProxylessRequestPayloadBuilder
{
    public override string TypeName => "GeeTestTask";

    public override JObject Build(GeeTestV4ProxylessRequest request)
    {
        var proxyRequest = (GeeTestV4Request)request;
        var payload = base.Build(request)
                .With(proxyRequest.ProxyConfig)
                .WithUserAgent(proxyRequest.UserAgent);
        
        
        if (proxyRequest.InitParameters != null && proxyRequest.InitParameters.Count > 0)
        {
            payload["initParameters"] = JObject.FromObject(proxyRequest.InitParameters);
        }

        return payload;
    }

}