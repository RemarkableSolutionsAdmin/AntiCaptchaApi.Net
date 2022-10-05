using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Serializers.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

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