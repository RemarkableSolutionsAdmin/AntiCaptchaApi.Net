namespace AntiCaptchaApi.Internal.Validation.ValidationErrors;

public record ValidationError(string PropertyName, string ErrorMessage)
{
    public override string ToString()
    {
        return $"{PropertyName} {ErrorMessage}";
    }
}