using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests.Abstractions;

namespace AntiCaptchaApi.Internal.Validation.Validators.Base;

public abstract class CaptchaRequestValidator<T> where T : CaptchaRequest
{
    public virtual ValidationResult Validate(T request) => new();
}