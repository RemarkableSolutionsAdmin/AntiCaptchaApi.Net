using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

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