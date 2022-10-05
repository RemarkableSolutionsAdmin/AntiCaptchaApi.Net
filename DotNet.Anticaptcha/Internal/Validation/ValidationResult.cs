using System.Collections.Generic;
using DotNet.Anticaptcha.Internal.Validation.ValidationErrors;

namespace DotNet.Anticaptcha.Internal.Validation;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<ValidationError> Errors { get; set; } = new();
}