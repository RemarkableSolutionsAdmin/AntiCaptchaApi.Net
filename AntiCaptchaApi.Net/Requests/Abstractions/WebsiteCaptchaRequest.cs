using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Requests.Abstractions
{
    public abstract class WebsiteCaptchaRequest<TSolution> : CaptchaRequest<TSolution>, IWebCaptchaRequest<TSolution>
        where TSolution : BaseSolution  
    {
        /// <summary>
        /// [Required]
        /// Recaptcha website key. Learn how to find it in this article.
        /// https://anti-captcha.com/apidoc/articles/how-to-find-the-sitekey
        /// </summary>
        public string WebsiteKey { get; set; }
    }
}