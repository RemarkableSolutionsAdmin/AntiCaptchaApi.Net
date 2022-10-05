using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Serializers.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

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