using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Requests;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators;

public class HCaptchaRequestValidator : HCaptchaProxylessRequestValidator
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateProxy(((HCaptchaRequest)request).ProxyConfig);
}