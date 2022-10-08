using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Validation.Validators;

public class HCaptchaRequestValidator : HCaptchaProxylessRequestValidator
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateProxy(((HCaptchaRequest)request).ProxyConfig);
}