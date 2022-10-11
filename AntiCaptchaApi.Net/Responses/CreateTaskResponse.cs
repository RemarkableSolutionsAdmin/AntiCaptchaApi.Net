using AntiCaptchaApi.Net.Responses.Abstractions;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Responses
{
    public sealed class CreateTaskResponse : BaseResponse
    {
        
        public CreateTaskResponse()
        {
            
        }
        public CreateTaskResponse(string errorCode, string errorDescription) : base(errorCode, errorDescription) { }
        
        [JsonProperty("taskId")]
        public int? TaskId { get; internal set; }
    }
}