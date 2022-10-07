using DotNet.Anticaptcha.Responses.Abstractions;

namespace DotNet.Anticaptcha.Responses;

public class ActionResponse : BaseResponse
{
    public string Status { get; set; }
}