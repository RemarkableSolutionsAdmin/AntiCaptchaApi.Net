using System.Threading.Tasks;
using DotNet.Anticaptcha.Enums;
using DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests;

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