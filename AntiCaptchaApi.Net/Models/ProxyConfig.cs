using AntiCaptchaApi.Net.Enums;

namespace AntiCaptchaApi.Net.Models
{
    public class ProxyConfig 
    {
        public string ProxyLogin {  get; set; }
        public string ProxyPassword {  get; set; }
        public int? ProxyPort { get; set; }
        public ProxyTypeOption ProxyType {  get; set; }
        public string ProxyAddress { get; set; }
    }
}