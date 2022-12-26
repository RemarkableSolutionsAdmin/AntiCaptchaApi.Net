﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Solve Arkose Labs captcha (aka Funcaptcha) automatically without proxy - FuncaptchaTaskProxyless
    /// This type of task solves Arkose Labs captcha (or Funcaptcha) without proxy. \
    /// Task will be executed using our own proxy servers and/or workers' IP addresses.
    ///
    /// Arkose Labs API provides information to the website owner about the solver's IP address.
    /// However it's worth trying first to bypass captcha without proxy, and if it doesn't work - switch to FuncaptchaTask with proxy.
    /// Example captcha: https://anti-captcha.com/_nuxt/img/funcaptcha1.e289a39.jpg
    /// </summary>
    public class FunCaptchaProxylessRequest : CaptchaRequest<FunCaptchaSolution>
    {
        /// <summary>
        /// [Required]
        /// Address of a target web page. Can be located anywhere on the web site, even in a member area.
        /// Our workers don't navigate there but simulate the visit instead.
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }
        
        /// <summary>
        /// [Required]
        /// Arkose Labs public key
        /// </summary>
        public string WebsitePublicKey { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Custom Arkose Labs subdomain from which the Javascript widget is loaded. Required for some cases, but most Arkose Labs integrations run without it.
        /// </summary>
        [JsonProperty("funcaptchaApiJSSubdomain")]
        public string FunCaptchaApiJsSubdomain { get; set; }
        
        /// <summary>
        /// [Optional]
        /// An additional parameter that may be required by Arkose Labs implementation.
        /// Use this property to send "blob" value as an object converted to a string. See an example of what it might look like.{"\blob\":\"HERE_COMES_THE_blob_VALUE\"}
        /// </summary>
        public string Data { get; set; }
    }
}