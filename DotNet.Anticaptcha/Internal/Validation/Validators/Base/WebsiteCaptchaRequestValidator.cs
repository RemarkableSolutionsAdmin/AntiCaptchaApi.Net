using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests.Abstractions;

namespace DotNet.Anticaptcha.Internal.Validation.Validators.Base;

public abstract class WebsiteCaptchaRequestValidator<T> : CaptchaRequestValidator<T> where T : WebsiteCaptchaRequest
{
    public override ValidationResult Validate(T request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteKey), request.WebsiteKey)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl);
}