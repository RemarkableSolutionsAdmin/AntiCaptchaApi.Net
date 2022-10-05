namespace DotNet.Anticaptcha.Models.Solutions;

public class GeeTestV4Solution : BaseSolution
{
    public string CaptchaId { get; internal set; }
    public string LotNumber { get; internal set; }
    public string PassToken { get; internal set; }
    public string GenTime { get; internal set; }
    public string CaptchaOutput { get; internal set; }

    public override bool IsValid() =>
        CaptchaId != null &&
        LotNumber != null &&
        PassToken != null &&
        GenTime != null &&
        CaptchaOutput != null;
}