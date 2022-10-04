using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Requests
{
    /// <summary>
    /// Bypass Recaptcha V2 proxyless
    /// Use this type of task to automatically solve Google Recaptcha V2. Result of the job is g-response value.
    /// Use it to submit the form at the target website.
    ///
    /// The task is executed using our own proxy servers and/or workers' IP addresses.
    /// At the moment, Recaptcha does not have protection from situations where a puzzle is solved on one IP address and the form with the g-response is submitted from another.
    /// Google's API does not provide the IP address of the person who solved their Recaptcha.
    /// If this changes, you may always use our standard type of task for that - RecaptchaV2Task.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/recaptcha-v2.db8dd45.png
    /// </summary>
    public class RecaptchaV2ProxylessRequest : WebsiteCaptchaRequest
    {
        /// <summary>
        /// [Optional]
        /// Value of 'data-s' parameter. Applies only to captchas on Google web sites.
        /// </summary>
        public string RecaptchaDataSValue { internal get; set; }
        
        /// <summary>
        /// [Optional]
        /// Specify whether or not Recaptcha is invisible. This will render an appropriate widget for our workers.
        /// </summary>
        public bool IsInvisible { internal get; set; }
    }
}