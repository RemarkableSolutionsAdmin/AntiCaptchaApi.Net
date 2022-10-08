using System.Collections.Generic;
using AntiCaptchaApi.Internal.Validation.ValidationErrors;

namespace AntiCaptchaApi.Internal.Validation;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<ValidationError> Errors { get; set; } = new();
}