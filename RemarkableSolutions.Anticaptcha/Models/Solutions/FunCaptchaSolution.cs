namespace RemarkableSolutions.Anticaptcha.Models.Solutions;

public class FunCaptchaSolution : BaseSolution
{
    public string Token { get; set; }
    public override bool IsValid() => Token != null;
}