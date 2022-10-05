using DotNet.Anticaptcha.Models;

namespace DotNet.Anticaptcha.Requests
{
    /// <summary>
    /// Solve HCaptcha automatically via a proxy - HCaptchaTask
    ///
    /// HCaptcha devs call their captcha "a drop-in replacement for Recaptcha".
    /// We tried to create the same thing in our API, so task properties are absolutely the same as in RecaptchaV2Task except for the "type" property.
    ///
    /// IMPORTANT: hCaptcha seems to have a limit on solved tasks from one IP: about 3 items per 12 hours.
    /// Take this into account when you build the solving process through your proxy.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/hcaptcha_example1.f7d96e5.png
    /// </summary>
    public class HCaptchaRequest : HCaptchaProxylessRequest
    {
        /// <summary>
        /// [Required] ProxyConfig.ProxyType : Type of proxy http http/socks4/socks4.
        /// [Required] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Required] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        public ProxyConfig ProxyConfig { internal get; set; } = new();
    }
}