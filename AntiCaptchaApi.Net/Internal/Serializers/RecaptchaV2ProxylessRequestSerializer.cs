using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

internal class RecaptchaV2ProxylessRequestSerializer: WebsiteCaptchaRequestSerializer<RecaptchaV2ProxylessRequest, RecaptchaSolution>
{
    public override string TypeName => "RecaptchaV2TaskProxyless";
    public override JObject Serialize(RecaptchaV2ProxylessRequest request)
    {            
        return base.Serialize(request)
            .With("recaptchaDataSValue", request.RecaptchaDataSValue)
            .With("isInvisible", request.IsInvisible);
    }
}