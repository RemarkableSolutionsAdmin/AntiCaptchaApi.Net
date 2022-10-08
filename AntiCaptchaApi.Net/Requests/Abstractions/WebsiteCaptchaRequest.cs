namespace AntiCaptchaApi.Requests.Abstractions
{
    public abstract class WebsiteCaptchaRequest : CaptchaRequest
    {
        /// <summary>
        /// [Required]
        /// Address of a target web page where our worker will navigate.
        /// </summary>
        public string WebsiteUrl { get; set; }
        /// <summary>
        /// [Required]
        /// Recaptcha website key. Learn how to find it in this article.
        /// https://anti-captcha.com/apidoc/articles/how-to-find-the-sitekey
        /// </summary>
        public string WebsiteKey { get; set; }
    }
}