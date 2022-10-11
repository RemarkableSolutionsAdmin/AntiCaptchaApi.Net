using System;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Converters;

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
            rawTaskResultResponse.Solution ??= new RawSolution();
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