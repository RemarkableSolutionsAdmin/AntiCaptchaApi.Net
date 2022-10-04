using System.Collections.Generic;
using RemarkableSolutions.Anticaptcha.Internal.Validation.ValidationErrors;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<ValidationError> Errors { get; set; } = new();
}