using System.Collections.Generic;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Bypass Google Recaptcha V2 Enterprise without proxy - RecaptchaV2EnterpriseTaskProxyless
    ///
    /// This type of task is for solving Google Recaptcha Enterprise V2 from the worker's IP address.
    ///
    /// It is mostly similar to RecaptchaV2TaskProxyless, except tasks are solved using an <see href="https://cloud.google.com/recaptcha-enterprise/docs">Enterprise API</see> 
    /// and assigned to workers with the best Recaptcha V3 score.
    /// For more information about Recaptcha Enterprise please visit <see href="https://anti-captcha.com/faq/494_everything_about_recaptcha_enterprise">this</see> FAQ section.
    /// </summary>
    public class RecaptchaV2EnterpriseProxylessRequest : WebsiteCaptchaRequest<RecaptchaSolution>
    {
        /// <summary>
        /// [Optional]
        /// Additional parameters which should be passed to "grecaptcha.enterprise.render" method along with sitekey.
        /// Example of what you should search for:
        ///     grecaptcha.enterprise.render("some-div-id", {
        ///        sitekey: "6Lc_aCMTAAAAABx7u2N0D1XnVbI_v6ZdbM6rYf16",
        ///        theme: "dark",
        ///        s: "2JvUXHNTnZl1Jb6WEvbDyBMzrMTR7oQ78QRhBcG07rk9bpaAaE0LRq1ZeP5NYa0N...ugQA"
        ///    });
        /// In this example, you will notice a parameter "s" which is not documented, but obviously required.
        /// Send it to the API, so that we render the Recaptcha widget with this parameter properly.
        ///</summary>
        public Dictionary<string, string> EnterprisePayload = new();
        
        
        
        /// <summary>
        /// [Optional]
        /// Use this parameter to send the domain name from which the Recaptcha script should be served.
        /// Can have only one of two values: "www.google.com" or "www.recaptcha.net".
        /// Do not use this parameter unless you understand what you are doing.
        /// </summary>
        public string ApiDomain { internal get; set; }
    }
}