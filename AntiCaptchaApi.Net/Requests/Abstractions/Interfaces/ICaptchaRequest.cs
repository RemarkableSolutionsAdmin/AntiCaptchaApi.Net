using AntiCaptchaApi.Net.Models.Solutions;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface ICaptchaRequest<TSolution> : IWebsiteUrlArg
    where TSolution : BaseSolution  
{
    
}