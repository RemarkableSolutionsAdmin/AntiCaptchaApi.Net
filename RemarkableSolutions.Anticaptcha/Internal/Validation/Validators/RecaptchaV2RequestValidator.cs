using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class RecaptchaV2RequestValidator : RecaptchaV2ProxylessRequestValidator
{
    public override ValidationResult Validate(RecaptchaV2ProxylessRequest request)
    {
        return base.Validate(request)
            .ValidateProxy(((RecaptchaV2Request)request).ProxyConfig)
            .ValidateIsNotNullOrEmpty(nameof(RecaptchaV2Request.UserAgent), ((RecaptchaV2Request)request).UserAgent);
    }
}