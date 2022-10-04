using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal class RecaptchaV2ProxylessRequestPayloadBuilder: WebsiteCaptchaRequestPayloadBuilder<RecaptchaV2ProxylessRequest>
{
    public override string TypeName => "RecaptchaV2TaskProxyless";
    public override JObject Build(RecaptchaV2ProxylessRequest request)
    {            
        return base.Build(request)
            .With("recaptchaDataSValue", request.RecaptchaDataSValue)
            .With("isInvisible", request.IsInvisible);
    }
}