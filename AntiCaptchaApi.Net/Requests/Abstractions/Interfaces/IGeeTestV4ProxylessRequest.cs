using System.Collections.Generic;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IGeeTestV4ProxylessRequest : IGeeTestArgs
{
    public Dictionary<string, string> InitParameters { get; set; }   
}