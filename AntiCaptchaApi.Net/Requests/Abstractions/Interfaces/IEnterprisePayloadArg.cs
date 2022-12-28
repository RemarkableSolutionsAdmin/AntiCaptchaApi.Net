using System.Collections.Generic;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IEnterprisePayloadArg : IRequestArg
{
    public Dictionary<string, string> EnterprisePayload { get; set; }
}