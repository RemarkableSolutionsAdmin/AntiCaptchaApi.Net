using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class RecaptchaV2RequestValidator : RecaptchaV2ProxylessRequestValidator
{
    public override ValidationResult Validate(RecaptchaV2ProxylessRequest request)
    {
        return base.Validate(request)
            .ValidateProxy(((RecaptchaV2Request)request).ProxyConfig)
            .ValidateIsNotNullOrEmpty(nameof(RecaptchaV2Request.UserAgent), ((RecaptchaV2Request)request).UserAgent);
    }
}