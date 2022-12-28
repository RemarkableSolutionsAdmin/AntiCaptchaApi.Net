using AntiCaptchaApi.Net.Models;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Extensions
{
    internal static class JObjectExtensions
    {
        internal static JObject WithUserAgent(this JObject @jObject, JToken value)
        {
            return jObject.With("userAgent", value);
        }
        
        internal static JObject WithCookies(this JObject @jObject, JToken value)
        {
            return jObject.With("cookies", value);
        }

        internal static JObject With(this JObject @jObject, string key, JToken value)
        {
            @jObject.Add(key, value);
            return @jObject;
        }
        
        internal static JObject WithIf(this JObject @jObject, string key, JToken value, bool condition)
        {
            if(condition)
                @jObject.Add(key, value);
            return @jObject;
        }
        
        internal static JObject With(this JObject @jObject, ProxyConfig proxyConfig)
        {
            if(proxyConfig is ProxyConfig ProxyConfig)
                if(!string.IsNullOrEmpty(ProxyConfig.ProxyType.ToString()))
                    jObject.Add("proxyType", ProxyConfig.ProxyType.ToString().ToLower());
            
            if(!string.IsNullOrEmpty(proxyConfig.ProxyAddress))
                jObject.Add("proxyAddress", proxyConfig.ProxyAddress);
            
            if(!string.IsNullOrEmpty(proxyConfig.ProxyPort.ToString()))
                jObject.Add("proxyPort", proxyConfig.ProxyPort);
            
            if(!string.IsNullOrEmpty(proxyConfig.ProxyLogin))
                jObject.Add("proxyLogin", proxyConfig.ProxyLogin);
            
            if(!string.IsNullOrEmpty(proxyConfig.ProxyPassword))
                jObject.Add("proxyPassword", proxyConfig.ProxyPassword);
            return @jObject;
        }
        
        internal static JObject WithIf(this JObject @jObject, ProxyConfig proxyConfig, bool condition)
        {
            if (condition)
            {
                jObject.With(proxyConfig);
            }
            return @jObject;
        }

        
    }
}