using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Validation.Validators.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class RecaptchaV3ProxylessRequestValidator : WebsiteCaptchaRequestValidator<RecaptchaV3ProxylessRequest>
{
    public override ValidationResult Validate(RecaptchaV3ProxylessRequest request)
    {
        return base.Validate(request)
            .ValidateIsOneOfTheValues(nameof(request.MinScore), request.MinScore, new []{0.3, 0.5, 0.7})
            .ValidateIsNotNull(nameof(request.IsEnterprise), request.IsEnterprise);
    }
}