using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Internal.Extensions;
using AntiCaptchaApi.Internal.Serializers.Base;
using AntiCaptchaApi.Requests;

namespace AntiCaptchaApi.Internal.Serializers;

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