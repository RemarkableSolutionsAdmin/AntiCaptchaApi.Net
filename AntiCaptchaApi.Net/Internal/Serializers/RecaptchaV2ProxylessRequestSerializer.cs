using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Serializers.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

internal class RecaptchaV2ProxylessRequestSerializer: WebsiteCaptchaRequestSerializer<RecaptchaV2ProxylessRequest>
{
    public override string TypeName => "RecaptchaV2TaskProxyless";
    public override JObject Serialize(RecaptchaV2ProxylessRequest request)
    {            
        return base.Serialize(request)
            .With("recaptchaDataSValue", request.RecaptchaDataSValue)
            .With("isInvisible", request.IsInvisible);
    }
}