using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Validation.Validators;

public class GeeTestV4RequestValidator : GeeTestV4ProxylessRequestValidator
{
    public override ValidationResult Validate(GeeTestV4ProxylessRequest request)
    {
        var proxyRequest = (GeeTestV4Request)request;
        return base.Validate(request)
            .ValidateProxy(proxyRequest.ProxyConfig)
            .ValidateIsNotNullOrEmpty(nameof(GeeTestV3Request.UserAgent), proxyRequest.UserAgent);
    }
}