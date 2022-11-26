using AntiCaptchaApi.Net.Models.Solutions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Requests.Abstractions
{
    public abstract class CaptchaRequest<TSolution> where TSolution : BaseSolution  
    {
        
    }
}