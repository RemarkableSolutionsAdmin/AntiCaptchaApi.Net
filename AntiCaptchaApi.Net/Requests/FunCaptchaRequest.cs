using AntiCaptchaApi.Net.Models;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Bypass Arkose Labs captcha (aka Funcaptcha) with proxy
    /// This type of task solves arkoselabs.com puzzles in our workers' browsers.
    /// Your app submits the website address and public key and receives a token after task completion.
    /// Use this token to submit the form with the Arkose Labs captcha.
    /// Example captcha: https://anti-captcha.com/_nuxt/img/funcaptcha1.e289a39.jpg
    /// </summary>
    public class FunCaptchaRequest : FunCaptchaProxylessRequest
    {
        
        /// <summary>
        /// [Required]
        /// Browser's User-Agent used in emulation.
        /// You must use a modern-browser signature; otherwise, Google will ask you to "update your browser".
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// [Required] ProxyConfig.ProxyType : Type of proxy http http/socks4/socks4.
        /// [Required] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Required] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        public TypedProxyConfig ProxyConfig { get; set; }
    }
}