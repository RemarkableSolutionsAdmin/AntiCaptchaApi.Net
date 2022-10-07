using System.Threading.Tasks;
using DotNet.Anticaptcha;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests.Abstractions;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Tests;
using DotNet.Anticaptcha.Tests.Helpers;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;

public abstract class AnticaptchaTestBase
{
    protected readonly AnticaptchaClient AnticaptchaClient = new(TestEnvironment.ClientKey);
    
    protected async Task TestCaptchaRequestAsync<T>(T captchaRequest) where T : CaptchaRequest
    {
        var creationTaskResult = await AnticaptchaClient.CreateCaptchaTaskAsync(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        var taskResult = await AnticaptchaClient.WaitForTaskRawResultAsync<RawSolution>(creationTaskResult.TaskId.Value);
        AssertHelper.Assert(taskResult);
    }

    protected void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest)
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest<TRequest, TSolution>(captchaRequest, out var creationResult, out var taskResult);
    }
    
    protected void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out CreateTaskResponse creationTaskResult) 
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest<TRequest, TSolution>(captchaRequest, out creationTaskResult, out var taskResult);
    }
    
    protected void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out CreateTaskResponse creationTaskResult, out TaskResultResponse<TSolution> rawTaskResult)
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        creationTaskResult = AnticaptchaClient.CreateCaptchaTask(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        rawTaskResult = AnticaptchaClient.WaitForRawTaskResult<TSolution>(creationTaskResult.TaskId.Value, 1800);
        AssertHelper.Assert(rawTaskResult);
    }
    
    protected void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out TaskResultResponse<TSolution> rawTaskResult) 
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest(captchaRequest, out var creationTaskResult, out rawTaskResult);
    }
}