using System;
using DotNet.Anticaptcha.Enums;
using DotNet.Anticaptcha.Models;

namespace DotNet.Anticaptcha.Tests
{
    public static class TestEnvironment
    {
        internal static string ClientKey => Environment.GetEnvironmentVariable("ClientKey");
        internal static string ProxyAddress => Environment.GetEnvironmentVariable("ProxyAddress");
        internal static string ProxyPort => Environment.GetEnvironmentVariable("ProxyPort");
        internal static string ProxyLogin => Environment.GetEnvironmentVariable("ProxyLogin");
        internal static string ProxyPassword => Environment.GetEnvironmentVariable("ProxyPassword");

        public static bool IsProxyDefined => 
            !string.IsNullOrEmpty(ProxyAddress) &&
            !string.IsNullOrEmpty(ProxyPort) &&
            !string.IsNullOrEmpty(ProxyLogin) &&
            !string.IsNullOrEmpty(ProxyPassword);
        
        
        
        internal static ProxyConfig GetCurrentTestProxyConfig()
        {
            return new ProxyConfig()
            {
                ProxyType = ProxyTypeOption.Http,
                ProxyAddress = ProxyAddress,
                ProxyPort = int.Parse(ProxyPort),
                ProxyLogin = ProxyLogin,
                ProxyPassword = ProxyPassword
            };
        }

        internal const string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116";
    }
}