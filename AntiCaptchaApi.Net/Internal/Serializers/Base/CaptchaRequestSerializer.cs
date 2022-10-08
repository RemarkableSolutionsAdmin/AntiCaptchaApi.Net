using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Requests.Abstractions;

namespace AntiCaptchaApi.Internal.Serializers.Base;


internal abstract class CaptchaRequestSerializer<TRequest> where TRequest : CaptchaRequest
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
