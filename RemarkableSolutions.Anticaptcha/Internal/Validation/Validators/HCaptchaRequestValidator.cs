using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class HCaptchaRequestValidator : HCaptchaProxylessRequestValidator
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateProxy(((HCaptchaRequest)request).ProxyConfig);
}