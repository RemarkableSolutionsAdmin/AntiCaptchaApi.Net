using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.Base;

[Collection("Sequential")]
public class AnticaptchaTestBase
{
    protected readonly AnticaptchaClient AnticaptchaClient = new(TestEnvironment.ClientKey);
}