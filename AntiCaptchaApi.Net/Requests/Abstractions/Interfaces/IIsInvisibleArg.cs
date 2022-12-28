namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IIsInvisibleArg : IRequestArg
{
    public bool? IsInvisible { get; set; }
}