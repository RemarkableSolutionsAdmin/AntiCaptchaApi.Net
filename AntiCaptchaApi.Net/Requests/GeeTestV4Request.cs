using System.Collections.Generic;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Solve GeeTest captcha V4 automatically without proxy
    /// This type of task solves GeeTest captchas in our workers' browsers.
    /// Your app submits the website address, gt key, challenge key.
    /// For version GeeTest version 4 output consists of 5 values
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/geetest_example3.8c80ec3.png
    /// </summary>
    public class GeeTestV4Request : GeeTestV4ProxylessRequest, IGeeTestV4Request
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
        public ProxyConfig ProxyConfig { get; set; }

        public GeeTestV4Request()
        {
            
        }
        
        public GeeTestV4Request(IGeeTestV4Request request) : base(request)
        {
            UserAgent = request.UserAgent;
            ProxyConfig = request.ProxyConfig;
        }
    }
}