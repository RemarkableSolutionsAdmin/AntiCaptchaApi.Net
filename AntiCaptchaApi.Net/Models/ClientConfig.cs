namespace AntiCaptchaApi.Net.Models;

public class ClientConfig
{
    public int MaxWaitingTimeInSeconds { get; init; } = 120;
    
    public int SolveAsyncMaxRetries { get; init; } = 1;
    
    public int StepWaitingTimeInMilliseconds { get; init; } = 1000;
}