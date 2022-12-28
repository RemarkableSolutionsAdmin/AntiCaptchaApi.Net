using AntiCaptchaApi.Net.Enums;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Models
{
    public class ProxyConfig 
    {
        public ProxyTypeOption ProxyType {  get; set; }
        public string ProxyLogin {  get; set; }
        public string ProxyPassword {  get; set; }
        public int? ProxyPort { get; set; }
        public string ProxyAddress { get; set; }
    }
}