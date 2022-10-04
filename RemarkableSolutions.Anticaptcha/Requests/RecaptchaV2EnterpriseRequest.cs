using RemarkableSolutions.Anticaptcha.Models;

namespace RemarkableSolutions.Anticaptcha.Requests
{
    /// <summary>
    /// Solve Google Recaptcha V2 Enterprise with a proxy - RecaptchaV2EnterpriseTask
    ///
    /// This type of task is for solving Google Recaptcha Enterprise V2 using the provided proxy.
    ///
    /// It is mostly similar to RecaptchaV2Task, except tasks are solved using an <see href="https://cloud.google.com/recaptcha-enterprise/docs">Enterprise API</see> and assigned to workers with the best Recaptcha V3 score.
    /// For more information about Recaptcha Enterprise please visit <see href="https://anti-captcha.com/faq/494_everything_about_recaptcha_enterprise">this</see> FAQ section.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/recaptcha-v2.db8dd45.png
    /// </summary>
    
    public class RecaptchaV2EnterpriseRequest : RecaptchaV2EnterpriseProxylessRequest
    {
        /// <summary>
        /// [Required] ProxyConfig.ProxyType : Type of proxy http http/socks4/socks4.
        /// [Required] ProxyConfig.proxyAddress : Proxy IP address ipv4/ipv6. No host names or IP addresses from local networks.
        /// [Required] ProxyConfig.proxyPort : Proxy port.
        /// [Optional] ProxyConfig.proxyLogin : Login for proxy which requires authorization (basic)
        /// [Optional] ProxyConfig.proxyPassword : Proxy password
        /// </summary>
        public ProxyConfig ProxyConfig { internal get; set; } = new();
        
        /// <summary>
        /// [Required]
        /// Browser's User-Agent used in emulation.
        /// You must use a modern-browser signature; otherwise, Google will ask you to "update your browser".
        /// </summary>
        public string UserAgent { internal get; set; }
        
        /// <summary>
        /// [Optional] 	Additional cookies that we should use in Google domains.
        /// </summary>
        public string Cookies { internal get; set; }
    }
}