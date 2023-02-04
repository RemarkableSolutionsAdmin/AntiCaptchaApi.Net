using AntiCaptchaApi.Net.Responses.Abstractions;

namespace AntiCaptchaApi.Net.Internal.Models
{
    internal abstract class Payload <TResponse>
        where TResponse : BaseResponse, new()
    {
        protected const int DefaultSoftId = 1023;
    }
}