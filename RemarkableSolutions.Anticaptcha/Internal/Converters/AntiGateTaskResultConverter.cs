using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RemarkableSolutions.Anticaptcha.Enums;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Responses;

namespace RemarkableSolutions.Anticaptcha.Internal.Converters;

public class AntiGateTaskResultConverter : TaskResultConverter<AntiGateSolution>
{
    public override void WriteJson(JsonWriter writer, TaskResultResponse<AntiGateSolution> value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override TaskResultResponse<AntiGateSolution> ReadJson(JsonReader reader, Type objectType, TaskResultResponse<AntiGateSolution> existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var AntiGateTaskResultResponse = base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer);
        
        if (AntiGateTaskResultResponse is { Status: TaskStatusType.Ready })
        {
            ParseJObjects(jObject, AntiGateTaskResultResponse);
        }
        
        return AntiGateTaskResultResponse;
    }

    private static void ParseJObjects(JObject jObject, TaskResultResponse<AntiGateSolution> AntiGateTaskResultResponse)
    {
        AntiGateTaskResultResponse.Solution.Cookies = ParseSolutionJObject(jObject, "cookies");
        AntiGateTaskResultResponse.Solution.LocalStorage = ParseSolutionJObject(jObject, "localStorage");
        AntiGateTaskResultResponse.Solution.Fingerprint = ParseSolutionJObject(jObject, "fingerprint");
        AntiGateTaskResultResponse.Solution.HTMLsInBase64 = ParseSolutionJObject(jObject, "htmlsInBase64");
    }
    
}