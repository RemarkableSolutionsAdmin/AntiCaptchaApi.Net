﻿using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Net.Requests;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators;

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