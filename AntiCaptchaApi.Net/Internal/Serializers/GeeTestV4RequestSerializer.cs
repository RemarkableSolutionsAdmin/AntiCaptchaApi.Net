using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed class GeeTestV4RequestSerializer : GeeTestV4ProxylessRequestSerializer
{
    public override string TypeName => "GeeTestTask";

    public override JObject Serialize(GeeTestV4ProxylessRequest request)
    {
        var proxyRequest = (GeeTestV4Request)request;
        var payload = base.Serialize(request)
                .With(proxyRequest.ProxyConfig)
                .WithUserAgent(proxyRequest.UserAgent);
        
        
        if (proxyRequest.InitParameters != null && proxyRequest.InitParameters.Count > 0)
        {
            payload["initParameters"] = JObject.FromObject(proxyRequest.InitParameters);
        }

        return payload;
    }

}