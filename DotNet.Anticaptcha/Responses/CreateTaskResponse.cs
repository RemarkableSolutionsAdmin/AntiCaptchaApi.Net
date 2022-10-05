using Newtonsoft.Json;
using DotNet.Anticaptcha.Responses.Abstractions;

namespace DotNet.Anticaptcha.Responses
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