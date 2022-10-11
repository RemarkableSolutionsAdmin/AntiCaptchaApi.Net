using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers.Base;

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