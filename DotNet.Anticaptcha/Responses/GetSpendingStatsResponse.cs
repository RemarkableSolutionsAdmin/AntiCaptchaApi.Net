using System.Collections.Generic;
using DotNet.Anticaptcha.Models;
using DotNet.Anticaptcha.Responses.Abstractions;

namespace DotNet.Anticaptcha.Responses;

public class GetSpendingStatsResponse : BaseResponse
{
    public List<DailySpendingStats> AllDailySpendingStats { get; set; }
}