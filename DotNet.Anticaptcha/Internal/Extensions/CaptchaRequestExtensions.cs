using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Validation;
using DotNet.Anticaptcha.Requests.Abstractions;

namespace DotNet.Anticaptcha.Internal.Extensions
{
    internal static class CaptchaRequestExtensions
    {
        internal static JObject ToPayload<T>(this T request) where T : CaptchaRequest => CaptchaRequestPayloadBuilder.Build(request);


        internal static ValidationResult Validate<T>(this T request) where T : CaptchaRequest => CaptchaRequestPayloadBuilder.Validate(request);
    }
}