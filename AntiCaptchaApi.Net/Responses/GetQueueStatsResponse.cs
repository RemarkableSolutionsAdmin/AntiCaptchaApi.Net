using AntiCaptchaApi.Responses.Abstractions;

namespace AntiCaptchaApi.Responses;

public class GetQueueStatsResponse : BaseResponse
{
    public int Waiting { get; set; }
    public double Load { get; set; }
    public double Bid { get; set; }
    public double Speed { get; set; }
    public int Total { get; set; }
}