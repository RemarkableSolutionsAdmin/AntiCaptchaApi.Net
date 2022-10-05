using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests.Abstractions;

namespace DotNet.Anticaptcha.Internal.Serializers.Base;

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