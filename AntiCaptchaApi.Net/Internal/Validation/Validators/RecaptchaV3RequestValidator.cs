using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators;

public class RecaptchaV3RequestValidator : WebsiteCaptchaRequestValidator<RecaptchaV3Request, RecaptchaSolution>
{
    public override ValidationResult Validate(RecaptchaV3Request request)
    {
        return base.Validate(request)
            .ValidateIsOneOfTheValues(nameof(request.MinScore), request.MinScore, new []{0.3, 0.5, 0.7})
            .ValidateIsNotNull(nameof(request.IsEnterprise), request.IsEnterprise);
    }
}