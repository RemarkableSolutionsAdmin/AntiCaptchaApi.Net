using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Models.Solutions
{
    public class RawSolution : BaseSolution
    {
        public string GRecaptchaResponse { get; set; }
        
        public string GRecaptchaResponseMd5 { get; set; }
        public string Text { get; set; }
        
        public string Url { get; set; }
        public string Token { get; set; }
        public string Challenge { get; set; }
        public string Seccode { get; set; }
        public string Validate { get; set; }
        
        public List<int> CellNumbers = new();
        
        public string CaptchaId { get; set; }
        
        public string LotNumber { get; set; }
        
        public string PassToken { get; set; }
        
        public string GenTime { get; set; }
        
        public string CaptchaOutput { get; set; }

        public string Domain { get; set; }
        
        [JsonIgnore]
        public JObject Answers { get; set; }
        
        [JsonIgnore]
        public JObject Cookies { get; set; }
        
        [JsonIgnore]
        public JObject LocalStorage { get; set; }
        
        [JsonIgnore]
        public JObject Fingerprint { get; set; }
        
        [JsonIgnore]
        public JObject DomainsOfInterest { get; set; }

        public override bool IsValid()
        {
            return GRecaptchaResponse != null ||
                   Text != null ||
                   Answers != null ||
                   Token != null ||
                   Challenge != null ||
                   Seccode != null ||
                   Validate != null ||
                   CellNumbers.Count != 0 ||
                   LocalStorage != null ||
                   Cookies != null ||
                   Fingerprint != null;
        }
    }
}