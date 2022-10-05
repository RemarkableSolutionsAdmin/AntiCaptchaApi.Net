using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Responses;

namespace DotNet.Anticaptcha.Internal.Helpers;

public static class BaseTaskResultResponseBuilder
{
    public static TaskResultResponse<TSolution> Build<TSolution>(string errorCode, string errorMessage) 
        where TSolution : BaseSolution, new()
    {
        return new TaskResultResponse<TSolution>()
        {
            ErrorCode = errorCode,
            ErrorDescription = errorMessage
        };
    }
}