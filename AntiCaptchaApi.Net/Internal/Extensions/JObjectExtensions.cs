using AntiCaptchaApi.Net.Models;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Extensions
{
    internal static class JObjectExtensions
    {
        internal static void Add(this JObject @jObject, ProxyConfig proxyConfig)
        {
            jObject.Add("proxyType", proxyConfig.ProxyType.ToString().ToLower());
            jObject.Add("proxyAddress", proxyConfig.ProxyAddress);
            jObject.Add("proxyPort", proxyConfig.ProxyPort);
            jObject.Add("proxyLogin", proxyConfig.ProxyLogin);
            jObject.Add("proxyPassword", proxyConfig.ProxyPassword);
        }

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
            jObject.Add("proxyType", proxyConfig.ProxyType.ToString().ToLower());
            jObject.Add("proxyAddress", proxyConfig.ProxyAddress);
            jObject.Add("proxyPort", proxyConfig.ProxyPort);
            jObject.Add("proxyLogin", proxyConfig.ProxyLogin);
            jObject.Add("proxyPassword", proxyConfig.ProxyPassword);
            return @jObject;
        }

        internal static JObject WithIf(this JObject @jObject, ProxyConfig proxyConfig, bool condition)
        {
            jObject.Add("proxyType", proxyConfig.ProxyType.ToString().ToLower());
            jObject.Add("proxyAddress", proxyConfig.ProxyAddress);
            jObject.Add("proxyPort", proxyConfig.ProxyPort);
            jObject.Add("proxyLogin", proxyConfig.ProxyLogin);
            jObject.Add("proxyPassword", proxyConfig.ProxyPassword);
            return @jObject;
        }

        
    }
}