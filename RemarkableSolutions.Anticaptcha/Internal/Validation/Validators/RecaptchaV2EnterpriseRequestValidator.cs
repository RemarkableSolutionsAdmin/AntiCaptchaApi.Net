using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

public class RecaptchaV2EnterpriseRequestValidator : RecaptchaV2EnterpriseProxylessRequestValidator
{
    public override ValidationResult Validate(RecaptchaV2EnterpriseProxylessRequest request) =>
        base.Validate(request)
            .ValidateProxy(((RecaptchaV2EnterpriseRequest)request).ProxyConfig)
            .ValidateIsNotNullOrEmpty(nameof(RecaptchaV2EnterpriseRequest.UserAgent), ((RecaptchaV2EnterpriseRequest)request).UserAgent);
}