using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AntiCaptchaApi.Enums;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi.Responses;

namespace AntiCaptchaApi.Internal.Converters;

public class TaskResultConverter<T> : JsonConverter<TaskResultResponse<T>>
    where T : BaseSolution, new()
{
    protected JObject jObject;
    
    public override void WriteJson(JsonWriter writer, TaskResultResponse<T> value, JsonSerializer serializer)
    {
        
    }

    public override TaskResultResponse<T> ReadJson(JsonReader reader, Type objectType, TaskResultResponse<T> existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        jObject = JObject.Load(reader);
        var rawTaskResultResponse = jObject.ToObject<TaskResultResponse<T>>();


        if (rawTaskResultResponse is { Status: TaskStatusType.Ready })
        {
            var createTime = (double?)jObject["createTime"];
            var endTime = (double?)jObject["endTime"];
            rawTaskResultResponse.CreateTimeUtc = UnixTimeStampToDateTime(createTime);
            rawTaskResultResponse.EndTimeUtc = UnixTimeStampToDateTime(endTime);
        }
        
        return rawTaskResultResponse;
    }


    private static DateTime? UnixTimeStampToDateTime(double? unixTimeStamp)
    {
        if (unixTimeStamp == null)
            return null;

        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dtDateTime.AddSeconds((double)unixTimeStamp).ToUniversalTime();
    }
    
    protected static JObject ParseSolutionJObject(JObject jObject, string name)
    {
        try
        {
            if (jObject["solution"][name] is JObject obj)
                return obj;

            if (jObject["solution"][name] is not JArray array)
                return null;
            
            var content = new JProperty("array",array);
            return new JObject
            {
                content
            };

        }
        catch (Exception)
        {
            return null;
        }   
    }
}