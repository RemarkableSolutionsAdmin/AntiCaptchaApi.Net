using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests;

public class GetAppStatsTests : AnticaptchaTestBase
{
    [Fact]
    public async Task ShouldReturnCorrectBalance_WhenCallingAuthenticRequest()
    {
        var appStats = await AnticaptchaClient.GetAppStatsAsync(TestEnvironment.SoftId, AppStatsMode.Errors);
        Assert.NotNull(appStats);
        Assert.False(appStats.IsErrorResponse);
    }
}