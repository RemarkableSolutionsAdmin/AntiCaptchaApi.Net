using AntiCaptchaApi.Net.Internal.Validation;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Extensions
{
    internal static class CaptchaRequestExtensions
    {
        internal static JObject ToPayload<T>(this CaptchaRequest<T> request) where T : BaseSolution => CaptchaRequestPayloadBuilder.BuildNew(request);


        internal static ValidationResult Validate<T>(this CaptchaRequest<T> request) where T : BaseSolution => CaptchaRequestPayloadBuilder.Validate(request);
    }
}