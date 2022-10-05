using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests.Abstractions;

namespace DotNet.Anticaptcha.Internal.Validation.Validators.Base;

public abstract class CaptchaRequestValidator<T> where T : CaptchaRequest
{
    public virtual ValidationResult Validate(T request) => new();
}