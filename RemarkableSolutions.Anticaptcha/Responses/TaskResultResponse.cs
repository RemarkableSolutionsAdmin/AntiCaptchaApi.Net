using System;
using RemarkableSolutions.Anticaptcha.Enums;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Responses.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Responses;

public class TaskResultResponse<T> : BaseResponse where T : BaseSolution
{
    
    public TaskResultResponse(string errorCode, string errorDescription) : base(errorCode, errorDescription) {}

    public TaskResultResponse() : base()
    {
        
    }
    
    public TaskStatusType? Status { get; set; }
    public double? Cost { get; set; }
    public string Ip { get; set; }
    public DateTime? CreateTimeUtc { get; set; }
    public DateTime? EndTimeUtc { get; set; }
    public int? SolveCount { get; set; }
    public  T Solution { get; set; }
}