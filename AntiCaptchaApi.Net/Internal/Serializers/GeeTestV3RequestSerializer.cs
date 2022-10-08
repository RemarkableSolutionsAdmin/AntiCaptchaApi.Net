using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

internal sealed class GeeTestV3RequestSerializer : GeeTestV3ProxylessRequestSerializer
{
    public override string TypeName => "GeeTestTask";

    public override JObject Serialize(GeeTestV3ProxylessRequest request) =>
        base.Serialize(request)
            .With(((GeeTestV3Request)request).ProxyConfig)
            .WithUserAgent(((GeeTestV3Request)request).UserAgent);
}