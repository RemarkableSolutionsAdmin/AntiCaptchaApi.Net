namespace DotNet.Anticaptcha.Models.Solutions;

public class HCaptchaSolution : BaseSolution
{
    public string GRecaptchaResponse { get; set; }
    public string GRecaptchaResponseMd5 { get; set; }
    public override bool IsValid() =>
        GRecaptchaResponse != null;
}