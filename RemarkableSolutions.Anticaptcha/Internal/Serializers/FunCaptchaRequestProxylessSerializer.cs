using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Serializers.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.Serializers;

internal  class FunCaptchaRequestProxylessSerializer : CaptchaRequestSerializer<FunCaptchaRequestProxyless>
{
    public override string TypeName => "FunCaptchaTaskProxyless";

    public override JObject Serialize(FunCaptchaRequestProxyless request) =>
        base.Serialize(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("websitePublicKey", request.WebsitePublicKey)
            .With("funcaptchaApiJSSubdomain", request.FunCaptchaApiJsSubdomain)
            .With("data", request.Data);
}