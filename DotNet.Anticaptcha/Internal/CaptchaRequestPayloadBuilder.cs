﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Serializers;
using DotNet.Anticaptcha.Internal.Validation;
using DotNet.Anticaptcha.Internal.Validation.Validators;
using DotNet.Anticaptcha.Requests;
using DotNet.Anticaptcha.Requests.Abstractions;

namespace DotNet.Anticaptcha.Internal;

internal static class CaptchaRequestPayloadBuilder
{
    private static Func<JObject> GetCaptchaRequestCreationHandler<T>(T request) where T : CaptchaRequest
    {
        var @switch = new Dictionary<Type, Func<JObject>> {
            { typeof(AntiGateRequest), () => new AntiGateRequestSerializer().Serialize(request as AntiGateRequest) },
            { typeof(FunCaptchaRequest), () => new FunCaptchaRequestSerializer().Serialize(request as FunCaptchaRequest) },
            { typeof(FunCaptchaRequestProxyless), () => new FunCaptchaRequestProxylessSerializer().Serialize(request as FunCaptchaRequestProxyless) },
            { typeof(GeeTestV3Request), () => new GeeTestV3RequestSerializer().Serialize(request as GeeTestV3Request) },
            { typeof(GeeTestV3ProxylessRequest), () => new GeeTestV3ProxylessRequestSerializer().Serialize(request as GeeTestV3ProxylessRequest) },
            { typeof(GeeTestV4ProxylessRequest), () => new GeeTestV4ProxylessRequestSerializer().Serialize(request as GeeTestV4ProxylessRequest) },
            { typeof(GeeTestV4Request), () => new GeeTestV4RequestSerializer().Serialize(request as GeeTestV4Request) },
            { typeof(HCaptchaProxylessRequest), () => new HCaptchaProxylessRequestSerializer().Serialize(request as HCaptchaProxylessRequest) },
            { typeof(HCaptchaRequest), () => new HCaptchaRequestSerializer().Serialize(request as HCaptchaRequest) },
            { typeof(ImageToTextRequest), () => new ImageToTextRequestSerializer().Serialize(request as ImageToTextRequest) },
            { typeof(RecaptchaV2EnterpriseProxylessRequest), () => new RecaptchaV2EnterpriseProxylessRequestSerializer().Serialize(request as RecaptchaV2EnterpriseProxylessRequest) },
            { typeof(RecaptchaV2EnterpriseRequest), () => new RecaptchaV2EnterpriseRequestSerializer().Serialize(request as RecaptchaV2EnterpriseRequest) },
            { typeof(RecaptchaV2ProxylessRequest), () => new RecaptchaV2ProxylessRequestSerializer().Serialize(request as RecaptchaV2ProxylessRequest) },
            { typeof(RecaptchaV2Request), () => new RecaptchaV2RequestSerializer().Serialize(request as RecaptchaV2Request) },
            { typeof(RecaptchaV3ProxylessRequest), () => new RecaptchaV3ProxylessRequestSerializer().Serialize(request as RecaptchaV3ProxylessRequest) },
        };
        return @switch[typeof(T)];
    }
    
    
    private static Func<ValidationResult> GetCaptchaRequestCreationValidator<T>(T request) where T : CaptchaRequest
    {
        var @switch = new Dictionary<Type, Func<ValidationResult>> {
            { typeof(AntiGateRequest), () => new AntiGateRequestValidator().Validate(request as AntiGateRequest) },
            { typeof(FunCaptchaRequest), () => new FunCaptchaRequestValidator().Validate(request as FunCaptchaRequest) },
            { typeof(FunCaptchaRequestProxyless), () => new FunCaptchaProxylessRequestValidator().Validate(request as FunCaptchaRequestProxyless) },
            { typeof(GeeTestV3Request), () => new GeeTestV3RequestValidator().Validate(request as GeeTestV3Request) },
            { typeof(GeeTestV3ProxylessRequest), () => new GeeTestV3ProxylessRequestValidator().Validate(request as GeeTestV3ProxylessRequest) },
            { typeof(GeeTestV4Request), () => new GeeTestV4RequestValidator().Validate(request as GeeTestV4Request) },
            { typeof(GeeTestV4ProxylessRequest), () => new GeeTestV4ProxylessRequestValidator().Validate(request as GeeTestV4ProxylessRequest) },
            { typeof(HCaptchaProxylessRequest), () => new HCaptchaProxylessRequestValidator().Validate(request as HCaptchaProxylessRequest) },
            { typeof(HCaptchaRequest), () => new HCaptchaRequestValidator().Validate(request as HCaptchaRequest) },
            { typeof(ImageToTextRequest), () => new ImageToTextRequestValidator().Validate(request as ImageToTextRequest) },
            { typeof(RecaptchaV2EnterpriseProxylessRequest), () => new RecaptchaV2EnterpriseProxylessRequestValidator().Validate(request as RecaptchaV2EnterpriseProxylessRequest) },
            { typeof(RecaptchaV2EnterpriseRequest), () => new RecaptchaV2EnterpriseRequestValidator().Validate(request as RecaptchaV2EnterpriseRequest) },
            { typeof(RecaptchaV2ProxylessRequest), () => new RecaptchaV2ProxylessRequestValidator().Validate(request as RecaptchaV2ProxylessRequest) },
            { typeof(RecaptchaV2Request), () => new RecaptchaV2RequestValidator().Validate(request as RecaptchaV2Request) },
            { typeof(RecaptchaV3ProxylessRequest), () => new RecaptchaV3ProxylessRequestValidator().Validate(request as RecaptchaV3ProxylessRequest) },
        };
        return @switch[typeof(T)];
    }

    internal static ValidationResult Validate<T>(T request) where T : CaptchaRequest
    {
        return GetCaptchaRequestCreationValidator(request).Invoke();
    }

    internal static JObject Build<T>(T request) where T : CaptchaRequest
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var handler = GetCaptchaRequestCreationHandler(request);
        var payload = handler.Invoke();
        return payload;
    }
}