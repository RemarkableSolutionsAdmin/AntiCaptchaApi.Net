using RemarkableSolutions.Anticaptcha.Responses;

namespace RemarkableSolutions.Anticaptcha.Models.Solutions;

public abstract class BaseSolution
{
    public abstract bool IsValid();
    public CreateTaskResponse CreateTaskResponse { get; set; }
}