using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

internal sealed class GeeTestV3RequestSerializer : GeeTestV3ProxylessRequestSerializer
{
    public override string TypeName => "GeeTestTask";

    public override JObject Serialize(GeeTestV3ProxylessRequest request) =>
        base.Serialize(request)
            .With(((GeeTestV3Request)request).ProxyConfig)
            .WithUserAgent(((GeeTestV3Request)request).UserAgent);
}