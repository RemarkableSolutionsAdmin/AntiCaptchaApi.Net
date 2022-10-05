using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Validation.Validators.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class FunCaptchaProxylessRequestValidator : CaptchaRequestValidator<FunCaptchaRequestProxyless>
{
    public override ValidationResult Validate(FunCaptchaRequestProxyless request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsitePublicKey), request.WebsitePublicKey);
}