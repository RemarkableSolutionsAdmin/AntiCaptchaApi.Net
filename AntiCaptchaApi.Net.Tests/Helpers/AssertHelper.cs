using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;

namespace AntiCaptchaApi.Net.Tests.Helpers;

public static class AssertHelper
{
    private static void AssertBase(BaseResponse? baseResponse)
    {
        Xunit.Assert.NotNull(baseResponse);
        if (baseResponse != null)
        {
            if (!string.IsNullOrEmpty(baseResponse.ErrorDescription))
            {
                Xunit.Assert.Null(baseResponse.ErrorDescription);
                Xunit.Assert.Empty(baseResponse.ErrorDescription);
            }

            Xunit.Assert.False(baseResponse.IsErrorResponse);
        }
    }
    
    public static void Assert(CreateTaskResponse? createTaskResponse)
    {
        AssertBase(createTaskResponse);
        if (createTaskResponse != null)
        {
            Xunit.Assert.NotEqual(0, createTaskResponse.TaskId);   
        }
    }

    public static void Assert<TSolution>(TaskResultResponse<TSolution> taskResultResponse)
        where TSolution : BaseSolution, new()
    {
        AssertBase(taskResultResponse);
        Xunit.Assert.NotNull(taskResultResponse.Status);
        Xunit.Assert.NotNull(taskResultResponse.Cost);
        Xunit.Assert.NotNull(taskResultResponse.Ip);
        Xunit.Assert.NotNull(taskResultResponse.CreateTimeUtc);
        Xunit.Assert.NotNull(taskResultResponse.EndTimeUtc);
        Xunit.Assert.NotNull(taskResultResponse.SolveCount);
    }

    public static void NotNullNotEmpty(string value)
    {
        Xunit.Assert.NotNull(value);
        Xunit.Assert.NotEmpty(value);
    }
}