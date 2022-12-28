using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

namespace AntiCaptchaApi.Net.Requests
{
    /// <summary>
    /// Bypass Recaptcha V3 Enterprise
    ///
    ///
    /// As V3 Enterprise is virtually the same as V3 non-Enterprise, we decided to roll out it’s support within the usual V3 tasks.
    /// Differences between V3 Enterprise and V3 non-Enterprise:
    /// widget code is loaded via enterprise.js (vs api.js)
    /// user score retrieval is made with grecaptcha.enterprise.execute call (vs grecaptcha.execute)
    /// This type of task object is required to solve Google Recaptcha V3 on a worker's computer.
    /// This task is executed by our service using our own proxy servers and/or workers' IP addresses.
    ///
    /// Please note that there's a difference between Recaptcha V2-invisible and Recaptcha V3.
    /// They look the same, and it might be confusing.
    /// There's a quick way to determine the correct type: try solving it with our API as V2-invisible and V3.
    /// In one of your attempts, you'll get an error, and in the other you won't.
    ///
    /// We test our workers for their recaptcha "score" and put them in 3 groups (queues): 0.3, 0.7 and 0.9.
    /// Each might have slightly different pricing due to the number of idle workers.
    /// By passing property minScore you define which queue your task goes into.
    ///
    /// IsEnterprise is set to true automatically.
    /// 
    /// Most of our available workers have score 0.3, and the minority have 0.9.More information about Recaptcha V3 can be found in <see href="https://anti-captcha.com/faq/449_everything_about_recaptcha_v3">this</see> FAQ section.
    /// 
    /// /// Example captcha: https://anti-captcha.com/_nuxt/img/recaptchav3.1b1650f.jpg
    /// </summary>
    public class RecaptchaV3EnterpriseRequest : RecaptchaV3Request, IRecaptchaV3EnterpriseRequest
    {
        
        public RecaptchaV3EnterpriseRequest()
        {
        }

        public RecaptchaV3EnterpriseRequest(IRecaptchaV3EnterpriseRequest request) : base(request)
        {
            
        }

    }
}