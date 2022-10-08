using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Validation;
using AntiCaptchaApi.Requests.Abstractions;

namespace AntiCaptchaApi.Internal.Extensions
{
    internal static class CaptchaRequestExtensions
    {
        internal static JObject ToPayload<T>(this T request) where T : CaptchaRequest => CaptchaRequestPayloadBuilder.Build(request);


        internal static ValidationResult Validate<T>(this T request) where T : CaptchaRequest => CaptchaRequestPayloadBuilder.Validate(request);
    }
}