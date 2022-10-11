using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Net.Requests;

namespace AntiCaptchaApi.Net.Internal.Validation.Validators;

public class ImageToTextRequestValidator : CaptchaRequestValidator<ImageToTextRequest>
{
    public override ValidationResult Validate(ImageToTextRequest request) =>
        base.Validate(request)
            .ValidateIfNotNullWithSpecialMessage(nameof(request.BodyBase64), request.BodyBase64,
                $"BodyBase64 is created out of file located at {nameof(ImageToTextRequest.FilePath)} value.");
}