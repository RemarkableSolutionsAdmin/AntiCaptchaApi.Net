﻿using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Enums;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Serializers.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal sealed class ImageToTextRequestSerializer : CaptchaRequestSerializer<ImageToTextRequest>
{
    public override string TypeName => "ImageToTextTask";
    public override JObject Serialize(ImageToTextRequest request) =>
        base.Serialize(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("comment", request.Comment)
            .With("body", request.BodyBase64.Replace("\r", "").Replace("\n", ""))
            .With("phrase", request.Phrase)
            .With("case", request.Case)
            .With("numeric", request.Numeric.Equals(NumericOption.NoRequirements) ? 0 : request.Numeric.Equals(NumericOption.NumbersOnly) ? 1 : 2)
            .With("math", request.Math)
            .With("minLength", request.MinLength)
            .With("maxLength", request.MaxLength);
}