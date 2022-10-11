using AntiCaptchaApi.Net.Internal.Validation;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Extensions
{
    internal static class CaptchaRequestExtensions
    {
        internal static JObject ToPayload<T>(this T request) where T : CaptchaRequest => CaptchaRequestPayloadBuilder.Build(request);


        internal static ValidationResult Validate<T>(this T request) where T : CaptchaRequest => CaptchaRequestPayloadBuilder.Validate(request);
    }
}