using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

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