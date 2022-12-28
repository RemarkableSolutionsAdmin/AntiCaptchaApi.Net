using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Bypass Recaptcha V2 Proxyless
    ///
    /// Use this type of task to automatically solve Google Recaptcha V2. Result of the job is g-response value. Use it to submit the form at the target website.
    /// The task is executed using our own proxy servers and/or workers' IP addresses.
    /// At the moment, Recaptcha does not have protection from situations where a puzzle is solved on one IP address and the form with the g-response is submitted from another.
    /// Google's API does not provide the IP address of the person who solved their Recaptcha.
    /// If this changes, you may always use our standard type of task for that - RecaptchaV2Task.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/recaptcha-v2.db8dd45.png
    /// </summary>
    public class RecaptchaV2Request : RecaptchaV2ProxylessRequest, IRecaptchaV2Request
    {
        public RecaptchaV2Request()
        {
        }

        public RecaptchaV2Request(IRecaptchaV2Request request) : base(request)
        {
            ProxyConfig = request.ProxyConfig;
            UserAgent = request.UserAgent;
            Cookies = request.Cookies;
        }

        /// <summary>
        /// [Required] ProxyConfig.ProxyType : Type of proxy http http/socks4/socks4.
        /// [Required] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Required] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        /// 
        public TypedProxyConfig ProxyConfig { get; set; }

        /// <summary>
        /// [Required]
        /// Browser's User-Agent used in emulation.
        /// You must use a modern-browser signature; otherwise, Google will ask you to "update your browser".
        /// </summary>
        /// 
        public string UserAgent { get; set; }
        /// <summary>
        /// [Optional] 	Additional cookies that we should use in Google domains.
        /// </summary>
        public string Cookies { get; set; }
    }
}