using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators;

public class AntiGateRequestValidator : CaptchaRequestValidator<AntiGateRequest, AntiGateSolution>
{
    public override ValidationResult Validate(AntiGateRequest request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNull(nameof(request.Variables), request.Variables)
            .ValidateIsNotNullOrEmpty(nameof(request.TemplateName), request.TemplateName)
            .ValidateOptionalProxy(request.ProxyConfig);
}