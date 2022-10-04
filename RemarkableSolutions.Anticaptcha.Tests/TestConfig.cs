using System;

namespace RemarkableSolutions.Anticaptcha.Tests
{
    public static class TestConfig
    {
        public static string ClientKey => Environment.GetEnvironmentVariable("ClientKey");
        public static string ProxyAddress => Environment.GetEnvironmentVariable("ProxyAddress");
        public static string ProxyPort => Environment.GetEnvironmentVariable("ProxyPort");
        public static string ProxyLogin => Environment.GetEnvironmentVariable("ProxyLogin");
        public static string ProxyPassword => Environment.GetEnvironmentVariable("ProxyPassword");

        public static bool IsProxyDefined => 
            !string.IsNullOrEmpty(ProxyAddress) &&
            !string.IsNullOrEmpty(ProxyPort) &&
            !string.IsNullOrEmpty(ProxyLogin) &&
            !string.IsNullOrEmpty(ProxyPassword);
    }
}