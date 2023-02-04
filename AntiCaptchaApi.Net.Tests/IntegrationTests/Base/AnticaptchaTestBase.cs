using AntiCaptchaApi.Net.Internal.Common;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.Base;

[Collection("Sequential")]
public class AnticaptchaTestBase
{
    protected readonly IAnticaptchaClient AnticaptchaClient = new AnticaptchaClient(TestEnvironment.ClientKey);
}