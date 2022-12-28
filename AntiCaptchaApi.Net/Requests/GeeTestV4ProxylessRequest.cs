using System.Collections.Generic;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json;

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
    public class GeeTestV4ProxylessRequest : CaptchaRequest<GeeTestV4Solution>, IGeeTestV4ProxylessRequest
    {
        /// <summary>
        /// [Required]
        /// The domain public key, rarely updated.
        /// </summary>
        public string Gt { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Changing token key. Make sure you grab a fresh one for each captcha; otherwise, you'll be charged for an error task.
        /// </summary>
        public string Challenge { get; set; }
        
        /// <summary>
        /// Optional API subdomain. May be required for some implementations.
        /// https://anti-captcha.com/_nuxt/img/geetest_subdomain.ab5ed5e.png
        /// </summary>
        public string GeetestApiServerSubdomain { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Required for some implementations. Send the JSON encoded into a string. The value can be traced in browser developer tools.
        /// Put a breakpoint before calling the "initGeetest" function
        /// https://anti-captcha.com/_nuxt/img/geetest_lib.2a8ba99.png
        /// </summary>
        public string GeetestGetLib { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Additional initialization parameters
        /// </summary>
        public Dictionary<string, string> InitParameters { get; set; }
    }
}