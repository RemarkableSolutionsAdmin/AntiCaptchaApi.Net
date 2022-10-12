using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal sealed class RecaptchaV3ProxylessRequestSerializer: WebsiteCaptchaRequestSerializer<RecaptchaV3ProxylessRequest, RecaptchaSolution>
{       
    public override string TypeName => "RecaptchaV3TaskProxyless";
    public override JObject Serialize(RecaptchaV3ProxylessRequest request) =>
        base.Serialize(request)
            .With("apiDomain", request.ApiDomain)
            .With("pageAction", request.PageAction)
            .With("minScore", request.MinScore)
            .With("isEnterprise", request is RecaptchaV3EnterpriseRequest || request.IsEnterprise);
}