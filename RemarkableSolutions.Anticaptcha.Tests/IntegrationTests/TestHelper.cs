using RemarkableSolutions.Anticaptcha.Enums;
using RemarkableSolutions.Anticaptcha.Models;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests;

public static class TestHelper
{
    public static ProxyConfig GetCurrentTestProxyConfig()
    {
        return new ProxyConfig()
        {
            ProxyType = ProxyTypeOption.Http,
            ProxyAddress = TestConfig.ProxyAddress,
            ProxyPort = int.Parse(TestConfig.ProxyPort),
            ProxyLogin = TestConfig.ProxyLogin,
            ProxyPassword = TestConfig.ProxyPassword
        };
    }
    
}