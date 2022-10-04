using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Validation.Validators.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class GeeTestV4ProxylessRequestValidator : CaptchaRequestValidator<GeeTestV4ProxylessRequest>
{
    public override ValidationResult Validate(GeeTestV4ProxylessRequest request)
    {
        return base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNullOrEmpty(nameof(request.Gt), request.Gt);
    }
}