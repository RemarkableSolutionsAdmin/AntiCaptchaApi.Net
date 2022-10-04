using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal sealed class RecaptchaV3ProxylessRequestPayloadBuilder: WebsiteCaptchaRequestPayloadBuilder<RecaptchaV3ProxylessRequest>
{       
    public override string TypeName => "RecaptchaV3TaskProxyless";
    public override JObject Build(RecaptchaV3ProxylessRequest request)
    {
        return base.Build(request)
            .With("apiDomain", request.ApiDomain)
            .With("pageAction", request.PageAction)
            .With("minScore", request.MinScore)
            .With("isEnterprise", request.IsEnterprise);
    }
}