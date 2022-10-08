namespace AntiCaptchaApi.Internal.Validation.ValidationErrors;

internal record MustNotBeNullError(string PropertyName) : ValidationError(PropertyName, "must not be null!")
{
    public override string ToString()
    {
        return base.ToString();
    }
}