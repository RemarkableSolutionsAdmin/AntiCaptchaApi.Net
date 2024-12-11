namespace AntiCaptchaApi.Net.Models;

public class ClientConfig
{
    public int MaxWaitForTaskResultTimeMs { get; set; } = 120000;

    public int MaxHttpRequestTimeMs { get; set; } = 60000;

    public int SolveAsyncRetries { get; set; } = 1;

    public int DelayTimeBetweenCheckingTaskResultMs { get; set; } = 1000;
}