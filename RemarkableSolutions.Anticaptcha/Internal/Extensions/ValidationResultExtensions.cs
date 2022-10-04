﻿using System.Collections.Generic;
using System.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Validation;
using RemarkableSolutions.Anticaptcha.Internal.Validation.ValidationErrors;
using RemarkableSolutions.Anticaptcha.Models;

namespace RemarkableSolutions.Anticaptcha.Internal.Extensions;

public static class ValidationResultExtensions
{
    internal static ValidationResult ValidateIsNotNullOrEmpty(this ValidationResult result, string name, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            result.Errors.Add(new MustNotBeNullOrEmptyError(name));
        }

        return result;
    }
    
    internal static ValidationResult ValidateIsNotNull(this ValidationResult result, string name, object value)
    {
        if (value == null)
        {
            result.Errors.Add(new MustNotBeNullError(name));
        }

        return result;
    }

    internal static ValidationResult ValidateIsOneOfTheValues<T>(this ValidationResult result, string name, T value,
        IReadOnlyCollection<T> values)
    {
        if (values.All(x => !x.Equals(value)))
        {
            result.Errors.Add(new MustBeOneOfTheValuesError(name, values.Select(x => x.ToString()).ToList()));
        }
        
        return result;
    }

    internal static ValidationResult ValidateIfNotNullWithSpecialMessage(this ValidationResult result, string name, object value,
        string message)
    {
        if (value == null)
        {
            result.Errors.Add(new MustNotBeNullWithSpecialMessageError(name, message));
        }

        return result;
    }
    

    internal static ValidationResult ValidateProxy(this ValidationResult result, ProxyConfig proxyConfig)
    {
        if (proxyConfig == null)
        {
            result.ValidateIsNotNull(nameof(ProxyConfig), proxyConfig);
        }
        else
        {
            result.ValidateIsNotNullOrEmpty(nameof(proxyConfig.ProxyAddress), proxyConfig.ProxyAddress);
            result.ValidateIsNotNull(nameof(proxyConfig.ProxyType), proxyConfig.ProxyType);
            if (proxyConfig.ProxyPort is null or < 1 or >= 65535)
            {
                result.Errors.Add(new ValidationError(nameof(proxyConfig.ProxyPort), "must have value between 1 and 65534"));
            }
        }

        return result;
    }
    
    internal static ValidationResult ValidateOptionalProxy(this ValidationResult result, ProxyConfig proxyConfig)
    {    
        if (proxyConfig != null && string.IsNullOrEmpty(proxyConfig.ProxyAddress))
            return result;
        
        if (proxyConfig == null)
        {
            result.ValidateIsNotNull(nameof(ProxyConfig), proxyConfig);
        }
        else
        {
            
            result.ValidateIsNotNullOrEmpty(nameof(proxyConfig.ProxyAddress), proxyConfig.ProxyAddress);
            result.ValidateIsNotNull(nameof(proxyConfig.ProxyType), proxyConfig.ProxyType);
            if (proxyConfig.ProxyPort is null or < 1 or >= 65535)
            {
                result.Errors.Add(new ValidationError(nameof(proxyConfig.ProxyPort), "must have value between 1 and 65534"));
            }
        }

        return result;
    }
}