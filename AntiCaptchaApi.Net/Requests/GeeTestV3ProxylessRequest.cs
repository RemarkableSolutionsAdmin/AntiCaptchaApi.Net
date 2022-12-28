using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Solve GeeTest captcha automatically without proxy
    /// This type of task solves GeeTest captchas in our workers' browsers.
    /// Your app submits the website address, gt key, challenge key and after task completion receives a solution consisting of 3 tokens.
    ///
    /// Example captcha: https://anti-captcha.com/_nuxt/img/geetest_example3.8c80ec3.png
    /// </summary>
    public class GeeTestV3ProxylessRequest : CaptchaRequest<GeeTestV3Solution>, IGeeTestV3ProxylessRequest
    {
        /// <summary>
        /// [Required]
        /// The domain public key, rarely updated.
        /// </summary>
        public string Gt { get; set; }
        
        /// <summary>
        /// [Required]
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
        

        public GeeTestV3ProxylessRequest()
        {
            
        }
        
        public GeeTestV3ProxylessRequest(IGeeTestV3ProxylessRequest request) : base(request)
        {
            Gt = request.Gt;
            Challenge = request.Challenge;
            GeetestApiServerSubdomain = request.GeetestApiServerSubdomain;
            GeetestGetLib = request.GeetestGetLib;
        }
    }
}