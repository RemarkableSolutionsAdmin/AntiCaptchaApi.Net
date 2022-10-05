using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class FunCaptchaRequestValidator : FunCaptchaProxylessRequestValidator
{
    public override ValidationResult Validate(FunCaptchaRequestProxyless request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(FunCaptchaRequest.UserAgent), ((FunCaptchaRequest)request).UserAgent)
            .ValidateOptionalProxy(((FunCaptchaRequest)request).ProxyConfig);
}