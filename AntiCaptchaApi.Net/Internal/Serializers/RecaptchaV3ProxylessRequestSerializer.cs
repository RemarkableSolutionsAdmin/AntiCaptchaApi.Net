using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Serializers.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

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