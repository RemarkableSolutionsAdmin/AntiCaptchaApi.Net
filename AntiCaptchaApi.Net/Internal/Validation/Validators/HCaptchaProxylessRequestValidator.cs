using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Net.Requests;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators;

public class HCaptchaProxylessRequestValidator : WebsiteCaptchaRequestValidator<HCaptchaProxylessRequest>
{
    public override ValidationResult Validate(HCaptchaProxylessRequest request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.UserAgent), request.UserAgent);
}