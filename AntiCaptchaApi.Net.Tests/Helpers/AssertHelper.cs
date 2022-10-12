﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;

namespace AntiCaptchaApi.Net.Tests.Helpers;

public static class AssertHelper
{
    private static void AssertBase(BaseResponse baseResponse)
    {
        if (!string.IsNullOrEmpty(baseResponse.ErrorDescription))
        {
            Xunit.Assert.Null(baseResponse.ErrorDescription);
            Xunit.Assert.Empty(baseResponse.ErrorDescription);
        }
        Xunit.Assert.False(baseResponse.IsErrorResponse);
    }
    
    public static void Assert(CreateTaskResponse createTaskResponse)
    {
        AssertBase(createTaskResponse);
        Xunit.Assert.NotEqual(0, createTaskResponse.TaskId);
    }

    public static void Assert<TSolution>(TaskResultResponse<TSolution> rawTaskResultResponse)
        where TSolution : BaseSolution, new()
    {
        AssertBase(rawTaskResultResponse);
        Xunit.Assert.NotNull(rawTaskResultResponse.Status);
        Xunit.Assert.NotNull(rawTaskResultResponse.Cost);
        Xunit.Assert.NotNull(rawTaskResultResponse.Ip);
        Xunit.Assert.NotNull(rawTaskResultResponse.CreateTimeUtc);
        Xunit.Assert.NotNull(rawTaskResultResponse.EndTimeUtc);
        Xunit.Assert.NotNull(rawTaskResultResponse.SolveCount);
    }

    public static void NotNullNotEmpty(string value)
    {
        Xunit.Assert.NotNull(value);
        Xunit.Assert.NotEmpty(value);
    }
}