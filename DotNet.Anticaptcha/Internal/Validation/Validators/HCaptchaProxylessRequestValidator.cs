using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Validation.Validators.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class HCaptchaProxylessRequestValidator : WebsiteCaptchaRequestValidator<HCaptchaProxylessRequest>
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.UserAgent), request.UserAgent);
}