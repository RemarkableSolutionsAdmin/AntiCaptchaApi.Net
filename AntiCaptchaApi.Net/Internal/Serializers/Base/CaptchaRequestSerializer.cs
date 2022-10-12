using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers.Base;


internal abstract class CaptchaRequestSerializer<TRequest, TSolution> 
    where TSolution : BaseSolution
    where TRequest : CaptchaRequest<TSolution>
{
    public abstract string TypeName { get; }
    public virtual JObject Serialize(TRequest request)
    {
        return new JObject
        {
            {"type", TypeName},
        };
    }
}
