using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders.Base;
using RemarkableSolutions.Anticaptcha.Requests;

namespace RemarkableSolutions.Anticaptcha.Internal.RequestPayloadBuilders;

internal  class FunCaptchaRequestProxylessPayloadBuilder : CaptchaRequestPayloadBuilder<FunCaptchaRequestProxyless>
{
    public override string TypeName => "FunCaptchaTaskProxyless";

    public override JObject Build(FunCaptchaRequestProxyless request) =>
        base.Build(request)
            .With("websiteURL", request.WebsiteUrl)
            .With("websitePublicKey", request.WebsitePublicKey)
            .With("funcaptchaApiJSSubdomain", request.FunCaptchaApiJsSubdomain)
            .With("data", request.Data);
}