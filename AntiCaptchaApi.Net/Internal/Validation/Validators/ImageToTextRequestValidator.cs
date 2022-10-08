using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Validation.Validators.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Validation.Validators;

public class ImageToTextRequestValidator : CaptchaRequestValidator<ImageToTextRequest>
{
    public override ValidationResult Validate(ImageToTextRequest request) =>
        base.Validate(request)
            .ValidateIfNotNullWithSpecialMessage(nameof(request.BodyBase64), request.BodyBase64,
                $"BodyBase64 is created out of file located at {nameof(ImageToTextRequest.FilePath)} value.");
}