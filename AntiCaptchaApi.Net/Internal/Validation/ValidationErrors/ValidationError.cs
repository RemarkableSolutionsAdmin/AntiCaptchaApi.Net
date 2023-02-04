﻿namespace AntiCaptchaApi.Net.Internal.Validation.ValidationErrors;

public class ValidationError
{
    public string PropertyName { get; }
    public string ErrorMessage { get; }

    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
    public override string ToString()
    {
        return $"{PropertyName} {ErrorMessage}";
    }
}