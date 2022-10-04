using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class FunCaptchaRequestValidator : FunCaptchaProxylessRequestValidator
{
    public override ValidationResult Validate(FunCaptchaRequestProxyless request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(FunCaptchaRequest.UserAgent), ((FunCaptchaRequest)request).UserAgent)
            .ValidateOptionalProxy(((FunCaptchaRequest)request).ProxyConfig);
}