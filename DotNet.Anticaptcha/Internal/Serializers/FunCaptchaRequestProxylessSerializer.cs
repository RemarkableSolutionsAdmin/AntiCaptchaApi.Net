using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Serializers.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

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