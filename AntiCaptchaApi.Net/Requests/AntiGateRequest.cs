using System.Collections.Generic;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Requests
{
    public class AntiGateRequest : CaptchaRequest<AntiGateSolution>, IAntiGateRequest
    {
        /// <summary>
        /// [Required]
        /// Name of a scenario template from our database.
        /// You can use an existing template or create your own.
        /// You may search for an existing template below this table.
        /// https://anti-captcha.com/apidoc/task-types/AntiGateTask
        /// </summary>
        public string TemplateName { get; set; }
        
        /// <summary>
        /// [Optional]
        /// An object containing template's variables and their values.
        /// </summary>
        public JObject Variables { get; set; }

        /// <summary>
        /// [Optional]
        /// List of domain names where we should collect cookies and localStorage data.
        /// This list can also be defined statically when editing а template.
        /// </summary>
        public List<string> DomainsOfInterest  { get; set; }
        
        /// <summary>
        /// [Optional] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Optional] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        public ProxyConfig ProxyConfig { get; set; }
    }
}