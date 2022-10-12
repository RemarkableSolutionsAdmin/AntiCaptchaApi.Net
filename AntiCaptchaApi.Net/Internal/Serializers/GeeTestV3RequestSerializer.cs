using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed class GeeTestV3RequestSerializer : GeeTestV3ProxylessRequestSerializer
{
    public override string TypeName => "GeeTestTask";

    public override JObject Serialize(GeeTestV3ProxylessRequest request) =>
        base.Serialize(request)
            .With(((GeeTestV3Request)request).ProxyConfig)
            .WithUserAgent(((GeeTestV3Request)request).UserAgent);
}