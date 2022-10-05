using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers.Base;

internal abstract class WebsiteCaptchaRequestSerializer<TRequest> : CaptchaRequestSerializer<TRequest>
    where TRequest : WebsiteCaptchaRequest
{
    public override JObject Serialize(TRequest request)
    {
        return base.Serialize(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("websiteKey", request.WebsiteKey);
    }
}