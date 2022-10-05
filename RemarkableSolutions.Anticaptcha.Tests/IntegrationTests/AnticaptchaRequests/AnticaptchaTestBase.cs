using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;

public abstract class AnticaptchaTestBase
{
    protected readonly AnticaptchaManager _anticaptchaManager = new(TestEnvironment.ClientKey);
    
    protected async Task TestCaptchaRequestAsync<T>(T captchaRequest) where T : CaptchaRequest
    {
        var creationTaskResult = await _anticaptchaManager.CreateCaptchaTaskAsync(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        var taskResult = await _anticaptchaManager.WaitForTaskRawResultAsync<RawSolution>(creationTaskResult.TaskId.Value);
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
        creationTaskResult = _anticaptchaManager.CreateCaptchaTask(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        rawTaskResult = _anticaptchaManager.WaitForRawTaskResult<TSolution>(creationTaskResult.TaskId.Value, 1800);
        AssertHelper.Assert(rawTaskResult);
    }
    
    protected void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out TaskResultResponse<TSolution> rawTaskResult) 
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest(captchaRequest, out var creationTaskResult, out rawTaskResult);
    }
}