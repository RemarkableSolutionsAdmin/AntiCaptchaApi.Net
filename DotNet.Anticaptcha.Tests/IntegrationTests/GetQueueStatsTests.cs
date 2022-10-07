using System.Threading.Tasks;
using DotNet.Anticaptcha.Enums;
using DotNet.Anticaptcha.Tests.IntegrationTests.AnticaptchaRequests;
using Xunit;

namespace DotNet.Anticaptcha.Tests.IntegrationTests
{
    public class AnticaptchaGetQueueStatsTests : AnticaptchaTestBase
    {
        [Fact]
        public async Task ShouldReturnCorrectStats_WhenCallingAuthenticRequest()
        {
            var queueStats = await AnticaptchaClient.GetQueueStatsAsync(QueueType.RecaptchaV3s07);
            Assert.NotNull(queueStats);
            Assert.NotEqual(0, queueStats.Bid);
            Assert.NotEqual(0, queueStats.Load);
            Assert.NotEqual(0, queueStats.Speed);
            Assert.NotEqual(0, queueStats.Total);
            Assert.NotEqual(0, queueStats.Waiting);
        }
    }
}