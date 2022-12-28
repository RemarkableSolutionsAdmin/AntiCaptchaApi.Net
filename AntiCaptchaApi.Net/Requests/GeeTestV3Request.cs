using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Solve GeeTest captcha automatically without proxy
    /// This type of task solves GeeTest captchas in our workers' browsers.
    /// Your app submits the website address, gt key, challenge key and after task completion receives a solution consisting of 3 tokens.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/geetest_example3.8c80ec3.png
    /// </summary>
    public class GeeTestV3Request : GeeTestV3ProxylessRequest, IGeeTestV3Request
    {
        
        /// <summary>
        /// [Required]
        /// Browser's User-Agent used in emulation.
        /// You must use a modern-browser signature; otherwise, Google will ask you to "update your browser".
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// [Required] ProxyConfig.ProxyType : Automatically changed to https.
        /// [Required] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Required] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        public TypedProxyConfig ProxyConfig { get; set; }

        public GeeTestV3Request()
        {
            
        }
        
        public GeeTestV3Request(IGeeTestV3Request request) : base(request)
        {
            UserAgent = request.UserAgent;
            ProxyConfig = request.ProxyConfig;
        }
    }
}