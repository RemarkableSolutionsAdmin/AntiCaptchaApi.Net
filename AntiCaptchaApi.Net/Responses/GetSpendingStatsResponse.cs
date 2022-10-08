using System.Collections.Generic;
using AntiCaptchaApi.Models;
using AntiCaptchaApi.Responses.Abstractions;

namespace AntiCaptchaApi.Responses;

public class GetSpendingStatsResponse : BaseResponse
{
    public List<DailySpendingStats> AllDailySpendingStats { get; set; }
}