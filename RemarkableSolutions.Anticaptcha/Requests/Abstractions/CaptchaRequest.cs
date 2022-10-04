namespace RemarkableSolutions.Anticaptcha.Requests.Abstractions
{
    public abstract class CaptchaRequest
    {
        /// <summary>
        /// [Required]
        /// Client account key. Can be found in account settings. https://anti-captcha.com/clients/settings/apisetup
        /// </summary>
        public string ClientKey { set; get; }
    }
}