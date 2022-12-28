namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface IIsEnterprise : IRequestArg
{
    public bool? IsEnterprise { get; set; }
}