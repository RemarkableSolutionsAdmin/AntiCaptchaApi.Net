using DotNet.Anticaptcha.Responses;

namespace DotNet.Anticaptcha.Models.Solutions;

public abstract class BaseSolution
{
    public abstract bool IsValid();
    public CreateTaskResponse CreateTaskResponse { get; set; }
}