﻿namespace AntiCaptchaApi.Net.Models.Solutions;

public class HCaptchaSolution : BaseSolution
{
    public string GRecaptchaResponse { get; set; }
    public override bool IsValid() =>
        GRecaptchaResponse != null;
}