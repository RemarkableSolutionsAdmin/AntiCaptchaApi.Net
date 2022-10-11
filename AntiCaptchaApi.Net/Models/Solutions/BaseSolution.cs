using AntiCaptchaApi.Net.Responses;

namespace AntiCaptchaApi.Net.Models.Solutions;

public abstract class BaseSolution
{
    public abstract bool IsValid();
    public CreateTaskResponse CreateTaskResponse { get; set; }
}