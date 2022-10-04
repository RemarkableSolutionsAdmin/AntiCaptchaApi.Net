using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;


internal abstract class CaptchaRequestPayloadBuilder<TRequest> where TRequest : CaptchaRequest
{
    public abstract string TypeName { get; }
    public virtual JObject Build(TRequest request)
    {
        return new JObject
        {
            {"type", TypeName},
        };
    }
}
