using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Validation.Validators;

public class HCaptchaProxylessRequestValidator : WebsiteCaptchaRequestValidator<HCaptchaProxylessRequest>
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.UserAgent), request.UserAgent);
}