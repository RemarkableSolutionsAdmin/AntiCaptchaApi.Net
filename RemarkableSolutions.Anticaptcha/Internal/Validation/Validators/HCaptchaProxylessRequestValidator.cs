using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Validation.Validators.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class HCaptchaProxylessRequestValidator : WebsiteCaptchaRequestValidator<HCaptchaProxylessRequest>
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.UserAgent), request.UserAgent);
}