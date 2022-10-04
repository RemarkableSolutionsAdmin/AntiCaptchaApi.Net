using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Validation;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Internal.Extensions
{
    internal static class CaptchaRequestExtensions
    {
        internal static JObject ToPayload<T>(this T request) where T : CaptchaRequest => PayloadBuilder.Build(request);


        internal static ValidationResult Validate<T>(this T request) where T : CaptchaRequest => PayloadBuilder.Validate(request);
    }
}