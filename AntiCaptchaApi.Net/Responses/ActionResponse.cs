using AntiCaptchaApi.Responses.Abstractions;

namespace AntiCaptchaApi.Responses;

public class ActionResponse : BaseResponse
{
    public string Status { get; set; }
}