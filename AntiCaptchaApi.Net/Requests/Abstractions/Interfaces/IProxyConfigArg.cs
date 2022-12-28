using AntiCaptchaApi.Net.Models;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IProxyConfigArg : IProxyArg
{
    public ProxyConfig ProxyConfig { get; set; }
}