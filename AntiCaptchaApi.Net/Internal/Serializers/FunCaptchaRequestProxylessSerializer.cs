using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Serializers.Base;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Serializers;

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