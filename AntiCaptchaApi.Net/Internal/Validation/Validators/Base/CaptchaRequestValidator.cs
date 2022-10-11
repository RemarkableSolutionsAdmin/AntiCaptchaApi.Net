using AntiCaptchaApi.Net.Requests.Abstractions;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators.Base;

public abstract class CaptchaRequestValidator<T> where T : CaptchaRequest
{
    public virtual ValidationResult Validate(T request) => new();
}