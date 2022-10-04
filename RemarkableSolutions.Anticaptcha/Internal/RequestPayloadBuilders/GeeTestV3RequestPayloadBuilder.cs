using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed class GeeTestV3RequestPayloadBuilder : GeeTestV3ProxylessRequestPayloadBuilder
{
    public override string TypeName => "GeeTestTask";

    public override JObject Build(GeeTestV3ProxylessRequest request) =>
        base.Build(request)
            .With(((GeeTestV3Request)request).ProxyConfig)
            .WithUserAgent(((GeeTestV3Request)request).UserAgent);
}