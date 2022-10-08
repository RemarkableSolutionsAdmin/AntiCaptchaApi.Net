using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Validation.Validators;

internal class GeeTestV3ProxylessRequestValidator : CaptchaRequestValidator<GeeTestV3ProxylessRequest>
{
    public override ValidationResult Validate(GeeTestV3ProxylessRequest request)
    {
        return base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNullOrEmpty(nameof(request.Gt), request.Gt)
            .ValidateIsNotNullOrEmpty(nameof(request.Challenge), request.Challenge);
    }
}