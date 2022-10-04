using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;

internal class GeeTestV3RequestValidator : GeeTestV3ProxylessRequestValidator
{
    public override ValidationResult Validate(GeeTestV3ProxylessRequest request)
    {
        var proxyRequest = (GeeTestV3Request)request;

        return base.Validate(request)
            .ValidateProxy(proxyRequest.ProxyConfig)
            .ValidateIsNotNullOrEmpty(nameof(GeeTestV3Request.UserAgent), proxyRequest.UserAgent);
    }
}