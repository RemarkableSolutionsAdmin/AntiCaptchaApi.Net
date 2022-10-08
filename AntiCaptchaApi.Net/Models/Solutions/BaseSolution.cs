using AntiCaptchaApi.Responses;

namespace AntiCaptchaApi.Models.Solutions;

public abstract class BaseSolution
{
    public abstract bool IsValid();
    public CreateTaskResponse CreateTaskResponse { get; set; }
}