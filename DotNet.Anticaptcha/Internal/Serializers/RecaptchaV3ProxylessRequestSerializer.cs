using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Serializers.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

internal sealed class RecaptchaV3ProxylessRequestSerializer: WebsiteCaptchaRequestSerializer<RecaptchaV3ProxylessRequest>
{       
    public override string TypeName => "RecaptchaV3TaskProxyless";
    public override JObject Serialize(RecaptchaV3ProxylessRequest request)
    {
        return base.Serialize(request)
            .With("apiDomain", request.ApiDomain)
            .With("pageAction", request.PageAction)
            .With("minScore", request.MinScore)
            .With("isEnterprise", request.IsEnterprise);
    }
}