namespace AntiCaptchaApi.Net.Models;

public class ClientConfig
{
    public int MaxWaitingTimeInSeconds { get; set; } = 120;
    
    public int SolveAsyncMaxRetries { get; set; } = 1;
    
    public int StepWaitingTimeInMilliseconds { get; set; } = 1000;
}