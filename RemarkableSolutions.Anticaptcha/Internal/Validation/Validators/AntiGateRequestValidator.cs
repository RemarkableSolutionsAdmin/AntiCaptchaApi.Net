using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Validation.Validators.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class AntiGateRequestValidator : CaptchaRequestValidator<AntiGateRequest>
{
    public override ValidationResult Validate(AntiGateRequest request) =>
        base.Validate(request)
            .ValidateIsNotNullOrEmpty(nameof(request.WebsiteUrl), request.WebsiteUrl)
            .ValidateIsNotNull(nameof(request.Variables), request.Variables)
            .ValidateIsNotNullOrEmpty(nameof(request.TemplateName), request.TemplateName)
            .ValidateOptionalProxy(request.ProxyConfig);
}