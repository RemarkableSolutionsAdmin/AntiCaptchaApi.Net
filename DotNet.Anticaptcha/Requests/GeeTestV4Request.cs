using System.Collections.Generic;
using DotNet.Anticaptcha.Models;

namespace DotNet.Anticaptcha.Requests
{
    /// <summary>
    /// Solve GeeTest captcha V4 automatically without proxy
    /// This type of task solves GeeTest captchas in our workers' browsers.
    /// Your app submits the website address, gt key, challenge key.
    /// For version GeeTest version 4 output consists of 5 values
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/geetest_example3.8c80ec3.png
    /// </summary>
    public class GeeTestV4Request : GeeTestV4ProxylessRequest
    {
        /// <summary>
        /// [Required]
        /// Browser's User-Agent used in emulation.
        /// You must use a modern-browser signature; otherwise, Google will ask you to "update your browser".
        /// </summary>
        public string UserAgent { internal get; set; }
        
        /// <summary>
        /// [Required] ProxyConfig.ProxyType : Automatically changed to https.
        /// [Required] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Required] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        public ProxyConfig ProxyConfig { get; set; } = new();
        
        
        
        /// <summary>
        /// [Optional
        /// Additional initialization parameters
        /// </summary>
        public Dictionary<string, string> InitParameters = new();
    }
}