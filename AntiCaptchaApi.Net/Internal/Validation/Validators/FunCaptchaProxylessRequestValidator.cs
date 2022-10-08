using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Validation.Validators;

public class FunCaptchaProxylessRequestValidator : CaptchaRequestValidator<FunCaptchaRequestProxyless>
{
    public override ValidationResult Validate(FunCaptchaRequestProxyless request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsitePublicKey), request.WebsitePublicKey);
}