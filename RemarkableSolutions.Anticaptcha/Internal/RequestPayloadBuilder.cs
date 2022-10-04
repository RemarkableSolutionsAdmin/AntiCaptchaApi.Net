using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;
using RemarkableSolutions.Anticaptcha.Internal.Validation;
using RemarkableSolutions.Anticaptcha.Internal.Validation.Validators;
using RemarkableSolutions.Anticaptcha.Requests;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Internal;

internal static class PayloadBuilder
{
    private static Func<JObject> GetCaptchaRequestCreationHandler<T>(T request) where T : CaptchaRequest
    {
        var @switch = new Dictionary<Type, Func<JObject>> {
            { typeof(AntiGateRequest), () => new AntiGateRequestPayloadBuilder().Build(request as AntiGateRequest) },
            { typeof(FunCaptchaRequest), () => new FunCaptchaRequestPayloadBuilder().Build(request as FunCaptchaRequest) },
            { typeof(FunCaptchaRequestProxyless), () => new FunCaptchaRequestProxylessPayloadBuilder().Build(request as FunCaptchaRequestProxyless) },
            { typeof(GeeTestV3Request), () => new GeeTestV3RequestPayloadBuilder().Build(request as GeeTestV3Request) },
            { typeof(GeeTestV3ProxylessRequest), () => new GeeTestV3ProxylessRequestPayloadBuilder().Build(request as GeeTestV3ProxylessRequest) },
            { typeof(GeeTestV4ProxylessRequest), () => new GeeTestV4ProxylessRequestPayloadBuilder().Build(request as GeeTestV4ProxylessRequest) },
            { typeof(GeeTestV4Request), () => new GeeTestV4RequestPayloadBuilder().Build(request as GeeTestV4Request) },
            { typeof(HCaptchaProxylessRequest), () => new HCaptchaProxylessRequestPayloadBuilder().Build(request as HCaptchaProxylessRequest) },
            { typeof(HCaptchaRequest), () => new HCaptchaRequestPayloadBuilder().Build(request as HCaptchaRequest) },
            { typeof(ImageToTextRequest), () => new ImageToTextRequestPayloadBuilder().Build(request as ImageToTextRequest) },
            { typeof(RecaptchaV2EnterpriseProxylessRequest), () => new RecaptchaV2EnterpriseProxylessRequestPayloadBuilder().Build(request as RecaptchaV2EnterpriseProxylessRequest) },
            { typeof(RecaptchaV2EnterpriseRequest), () => new RecaptchaV2EnterpriseRequestPayloadBuilder().Build(request as RecaptchaV2EnterpriseRequest) },
            { typeof(RecaptchaV2ProxylessRequest), () => new RecaptchaV2ProxylessRequestPayloadBuilder().Build(request as RecaptchaV2ProxylessRequest) },
            { typeof(RecaptchaV2Request), () => new RecaptchaV2RequestPayloadBuilder().Build(request as RecaptchaV2Request) },
            { typeof(RecaptchaV3ProxylessRequest), () => new RecaptchaV3ProxylessRequestPayloadBuilder().Build(request as RecaptchaV3ProxylessRequest) },
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