using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests.Abstractions;

namespace AntiCaptchaApi.Internal.Serializers.Base;

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