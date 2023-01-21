using AntiCaptchaApi.Net.Internal.Validation;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Extensions
{
    internal static class CaptchaRequestExtensions
    {
        internal static JObject ToPayload<T>(this ICaptchaRequest<T> request) where T : BaseSolution => CaptchaPayloadBuilder.Build(request);


        internal static ValidationResult Validate<T>(this ICaptchaRequest<T> request) where T : BaseSolution => CaptchaPayloadBuilder.Validate(request);
    }
}