using System.Collections.Generic;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    ///  This type of task to solve HCaptcha automatically without proxy.
    /// Result of the task is a token, which you need to use at the target website form.
    /// HCaptcha devs call their captcha "a drop-in replacement for Recaptcha".
    /// We tried to create the same thing in our API, so task properties are absolutely the same as in RecaptchaV2TaskProxyless except for the "type" property.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/hcaptcha_example1.f7d96e5.png
    /// </summary>
    public class HCaptchaProxylessRequest : WebsiteCaptchaRequest<HCaptchaSolution>, IHCaptchaProxylessRequest
    {
        /// <summary>
        /// [Required]
        /// Browser's User-Agent used in emulation.
        /// You must use a modern-browser signature; otherwise, Google will ask you to "update your browser".
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Specify whether or not HCaptcha is invisible. This will render an appropriate widget for our workers.
        /// </summary>
        public bool? IsInvisible { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Additional parameters which we'll use to render HCaptcha widget for Enterprise version.
        /// Property	  Type	 Required
        ///  rqdata	     String	    No
        ///  sentry      Boolean    No
        ///  apiEndpoint String	    No
        ///  endpoint	 String	    No
        ///  reportapi	 String	    No
        ///  assethost	 String	    No
        ///  imghost	 String	    No
        /// </summary>
        public Dictionary<string, string> EnterprisePayload { get; set; }
    }
}