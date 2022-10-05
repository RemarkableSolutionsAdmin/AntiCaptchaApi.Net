using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class HCaptchaRequestValidator : HCaptchaProxylessRequestValidator
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateProxy(((HCaptchaRequest)request).ProxyConfig);
}