using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;
using RemarkableSolutions.Anticaptcha.Responses;
using RemarkableSolutions.Anticaptcha.Tests.Helpers;
using Xunit;

namespace RemarkableSolutions.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;

public abstract class AnticaptchaTestBase
{
    protected static async Task TestCaptchaRequestAsync<T>(T captchaRequest) where T : CaptchaRequest
    {
        var creationTaskResult = await AnticaptchaManager.CreateCaptchaTaskAsync(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        var taskResult = await AnticaptchaManager.WaitForTaskRawResultAsync<RawSolution>(creationTaskResult.TaskId.Value, captchaRequest.ClientKey);
        AssertHelper.Assert(taskResult);
    }

    protected static void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest)
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest<TRequest, TSolution>(captchaRequest, out var creationResult, out var taskResult);
    }
    
    protected static void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out CreateTaskResponse creationTaskResult) 
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest<TRequest, TSolution>(captchaRequest, out creationTaskResult, out var taskResult);
    }
    
    protected static void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out CreateTaskResponse creationTaskResult, out TaskResultResponse<TSolution> rawTaskResult)
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        creationTaskResult = AnticaptchaManager.CreateCaptchaTask(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        rawTaskResult = AnticaptchaManager.WaitForRawTaskResult<TSolution>(creationTaskResult.TaskId.Value, captchaRequest.ClientKey, 1800);
        AssertHelper.Assert(rawTaskResult);
    }
    
    protected static void TestCaptchaRequest<TRequest, TSolution>(TRequest captchaRequest, out TaskResultResponse<TSolution> rawTaskResult) 
        where TRequest : CaptchaRequest
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest(captchaRequest, out var creationTaskResult, out rawTaskResult);
    }
}