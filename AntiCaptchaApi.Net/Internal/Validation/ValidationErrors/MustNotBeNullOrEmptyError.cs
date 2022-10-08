namespace AntiCaptchaApi.Internal.Validation.ValidationErrors;

internal record MustNotBeNullOrEmptyError(string PropertyName) : ValidationError(PropertyName, "must not be null or empty!")
{
    public override string ToString()
    {
        return base.ToString();
    }
}