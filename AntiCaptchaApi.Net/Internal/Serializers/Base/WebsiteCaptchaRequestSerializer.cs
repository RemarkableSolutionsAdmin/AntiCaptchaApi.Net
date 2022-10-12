using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers.Base;

internal abstract class WebsiteCaptchaRequestSerializer<TRequest, TSolution> : CaptchaRequestSerializer<TRequest, TSolution>
    where TSolution : BaseSolution
    where TRequest : WebsiteCaptchaRequest<TSolution>
{
    public override JObject Serialize(TRequest request)
    {
        return base.Serialize(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("websiteKey", request.WebsiteKey);
    }
}