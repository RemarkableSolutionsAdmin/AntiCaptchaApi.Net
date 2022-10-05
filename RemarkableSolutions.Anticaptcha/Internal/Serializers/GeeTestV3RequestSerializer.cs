using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal sealed class GeeTestV3RequestSerializer : GeeTestV3ProxylessRequestSerializer
{
    public override string TypeName => "GeeTestTask";

    public override JObject Serialize(GeeTestV3ProxylessRequest request) =>
        base.Serialize(request)
            .With(((GeeTestV3Request)request).ProxyConfig)
            .WithUserAgent(((GeeTestV3Request)request).UserAgent);
}