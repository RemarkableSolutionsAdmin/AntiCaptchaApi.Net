using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Requests.Abstractions
{
    public abstract class CaptchaRequest<TSolution> : ICaptchaRequest<TSolution> 
        where TSolution : BaseSolution  
    {
        /// <summary>
        /// [Required]
        /// Address of a target web page where our worker will navigate.
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }

        public CaptchaRequest()
        {
            
        }

        public CaptchaRequest(ICaptchaRequest<TSolution> request)
        {
            WebsiteUrl = request.WebsiteUrl;
        }
    }
}