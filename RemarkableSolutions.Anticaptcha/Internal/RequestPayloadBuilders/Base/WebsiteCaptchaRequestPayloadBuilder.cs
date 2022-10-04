using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;

internal abstract class WebsiteCaptchaRequestPayloadBuilder<TRequest> : CaptchaRequestPayloadBuilder<TRequest>
    where TRequest : WebsiteCaptchaRequest
{
    public override JObject Build(TRequest request)
    {
        return base.Build(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("websiteKey", request.WebsiteKey);
    }
}