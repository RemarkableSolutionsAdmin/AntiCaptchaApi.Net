using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Validation.Validators.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class FunCaptchaProxylessRequestValidator : CaptchaRequestValidator<FunCaptchaRequestProxyless>
{
    public override ValidationResult Validate(FunCaptchaRequestProxyless request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsitePublicKey), request.WebsitePublicKey);
}