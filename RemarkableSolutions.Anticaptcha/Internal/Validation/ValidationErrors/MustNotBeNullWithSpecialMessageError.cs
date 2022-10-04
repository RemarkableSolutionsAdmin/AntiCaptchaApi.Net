namespace RemarkableSolutions.Anticaptcha.Internal.Validation.ValidationErrors;

internal record MustNotBeNullWithSpecialMessageError(string PropertyName, string Message) : MustNotBeNullError(PropertyName)
{
    public override string ToString()
    {
        return $"{base.ToString()} {Message}";
    }
}
