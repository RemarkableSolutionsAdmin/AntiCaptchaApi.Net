using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Enums;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi.Responses;

namespace AntiCaptchaApi.Internal.Converters;

public class RawTaskResultConverter : TaskResultConverter<RawSolution>
{
    public override void WriteJson(JsonWriter writer, TaskResultResponse<RawSolution> value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override TaskResultResponse<RawSolution> ReadJson(JsonReader reader, Type objectType, TaskResultResponse<RawSolution> existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var rawTaskResultResponse = base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer);
        
        if (rawTaskResultResponse is { Status: TaskStatusType.Ready })
        {
            ParseJObjects(jObject, rawTaskResultResponse);
        }
        
        return rawTaskResultResponse;
    }

    private static void ParseJObjects(JObject jObject, TaskResultResponse<RawSolution> rawTaskResultResponse)
    {
        rawTaskResultResponse.Solution.Answers = ParseSolutionJObject(jObject, "answers");
        rawTaskResultResponse.Solution.Cookies = ParseSolutionJObject(jObject, "cookies");
        rawTaskResultResponse.Solution.LocalStorage = ParseSolutionJObject(jObject, "localStorage");
        rawTaskResultResponse.Solution.Fingerprint = ParseSolutionJObject(jObject, "fingerprint");
        rawTaskResultResponse.Solution.DomainsOfInterest = ParseSolutionJObject(jObject, "domainsOfInterest");
    }
    
}