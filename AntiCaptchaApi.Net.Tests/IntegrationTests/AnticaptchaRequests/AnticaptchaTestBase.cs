using System.Threading.Tasks;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;

public abstract class AnticaptchaTestBase
{
    protected readonly AnticaptchaClient AnticaptchaClient = new(TestEnvironment.ClientKey);
    
    protected async Task TestCaptchaRequestAsync<TSolution>(CaptchaRequest<TSolution> captchaRequest)
        where TSolution : BaseSolution, new()
    {
        var creationTaskResult = await AnticaptchaClient.CreateCaptchaTaskAsync(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        var taskResult = await AnticaptchaClient.WaitForTaskResultAsync<TSolution>(creationTaskResult.TaskId.Value);
        AssertHelper.Assert(taskResult);
    }

    protected void TestCaptchaRequest<TSolution>(CaptchaRequest<TSolution> captchaRequest)
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest(captchaRequest, out var creationResult, out var taskResult);
    }
    
    protected void TestCaptchaRequest<TSolution>(CaptchaRequest<TSolution> captchaRequest, out CreateTaskResponse creationTaskResult) 
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest(captchaRequest, out creationTaskResult, out var taskResult);
    }
    
    protected void TestCaptchaRequest<TSolution>(CaptchaRequest<TSolution> captchaRequest, out CreateTaskResponse creationTaskResult, out TaskResultResponse<TSolution> rawTaskResult)
        where TSolution : BaseSolution, new()
    {
        creationTaskResult = AnticaptchaClient.CreateCaptchaTask(captchaRequest);
        AssertHelper.Assert(creationTaskResult);
        Assert.NotNull(creationTaskResult.TaskId);
        rawTaskResult = AnticaptchaClient.WaitForTaskResult<TSolution>(creationTaskResult.TaskId.Value, 1800);
        AssertHelper.Assert(rawTaskResult);
    }
    protected void TestCaptchaRequest<TSolution>(CaptchaRequest<TSolution> captchaRequest, out TaskResultResponse<TSolution> rawTaskResult)
        where TSolution : BaseSolution, new()
    {
        TestCaptchaRequest(captchaRequest, out var creationTaskResult, out rawTaskResult);
    }
}