using AntiCaptchaApi.Net.Models.Solutions;

namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IWebCaptchaRequest<TSolution> : ICaptchaRequest<TSolution>, IWebsiteKeyArg
    where TSolution : BaseSolution  
{
    
}