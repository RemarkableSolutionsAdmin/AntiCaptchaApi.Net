using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IAntiGateRequest : IWebsiteUrlArg, IProxyConfigArg
{
    public string TemplateName { get; set; }
    public JObject Variables { get; set; }
    public List<string> DomainsOfInterest  { get; set; }
}