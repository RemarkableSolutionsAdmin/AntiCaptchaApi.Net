using System.Threading.Tasks;
using AntiCaptchaApi.Enums;
using AntiCaptchaApi.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace AntiCaptchaApi.Tests.IntegrationTests;

public class GetAppStatsTests : AnticaptchaTestBase
{
    [Fact]
    public async Task ShouldReturnCorrectBalance_WhenCallingAuthenticRequest()
    {
        var appStats = await AnticaptchaClient.GetAppStatsAsync(TestEnvironment.SoftId, AppStatsMode.Errors);
        Assert.NotNull(appStats);
        Assert.True(appStats.HasNoErrors);
    }
}