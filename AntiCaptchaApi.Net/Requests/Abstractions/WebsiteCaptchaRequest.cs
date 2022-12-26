using AntiCaptchaApi.Net.Models.Solutions;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Requests.Abstractions
{
    public abstract class WebsiteCaptchaRequest<TSolution> : CaptchaRequest<TSolution>
        where TSolution : BaseSolution  
    {
        /// <summary>
        /// [Required]
        /// Address of a target web page where our worker will navigate.
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }
        /// <summary>
        /// [Required]
        /// Recaptcha website key. Learn how to find it in this article.
        /// https://anti-captcha.com/apidoc/articles/how-to-find-the-sitekey
        /// </summary>
        public string WebsiteKey { get; set; }
    }
}