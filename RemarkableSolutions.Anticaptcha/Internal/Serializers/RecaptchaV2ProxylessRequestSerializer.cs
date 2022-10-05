using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Serializers.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

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