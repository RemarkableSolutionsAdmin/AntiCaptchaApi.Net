using System.Collections.Generic;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.ValidationErrors;

internal record MustBeOneOfTheValuesError(string PropertyName, List<string> correctValues) : ValidationError(PropertyName, "do not have correct value.")
{
    public override string ToString() => $"{base.ToString()}. Correct values: {string.Join(',', correctValues)}.";
}