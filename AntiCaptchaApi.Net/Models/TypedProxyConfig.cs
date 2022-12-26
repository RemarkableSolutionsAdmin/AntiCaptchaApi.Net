using AntiCaptchaApi.Net.Enums;

namespace AntiCaptchaApi.Net.Models;

public class TypedProxyConfig : ProxyConfig
{
    public ProxyTypeOption ProxyType {  get; set; }
}