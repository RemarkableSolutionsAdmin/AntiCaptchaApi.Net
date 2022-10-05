﻿using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Serializers.Base;
using DotNet.Anticaptcha.Requests;

namespace DotNet.Anticaptcha.Internal.Serializers;

internal class HCaptchaProxylessRequestSerializer : WebsiteCaptchaRequestSerializer<HCaptchaProxylessRequest>
{
    public override string TypeName => "HCaptchaTaskProxyless";
    public override JObject Serialize(HCaptchaProxylessRequest request)
    {            
        var payload = base.Serialize(request)
            .WithUserAgent(request.UserAgent)
            .With("isInvisible", request.IsInvisible);

        if (request.EnterprisePayload.Count > 0)
        {
            payload["enterprisePayload"] = JObject.FromObject(request.EnterprisePayload);
        }

        return payload;
    }
}