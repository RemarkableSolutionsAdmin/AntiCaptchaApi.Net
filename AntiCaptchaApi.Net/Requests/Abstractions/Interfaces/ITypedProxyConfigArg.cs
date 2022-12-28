using AntiCaptchaApi.Net.Models;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface ITypedProxyConfigArg : IProxyArg
{
    public TypedProxyConfig ProxyConfig { get; set; }
}