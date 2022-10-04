using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators.Base;

public abstract class CaptchaRequestValidator<T> where T : CaptchaRequest
{
    public virtual ValidationResult Validate(T request) =>
        new ValidationResult()
            .ValidateIsNotNullOrEmpty(nameof(request.ClientKey), request.ClientKey);
}