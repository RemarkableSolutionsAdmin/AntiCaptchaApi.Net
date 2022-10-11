using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests.Abstractions;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators.Base;

public abstract class WebsiteCaptchaRequestValidator<T> : CaptchaRequestValidator<T> where T : WebsiteCaptchaRequest
{
    public override ValidationResult Validate(T request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteKey), request.WebsiteKey)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl);
}