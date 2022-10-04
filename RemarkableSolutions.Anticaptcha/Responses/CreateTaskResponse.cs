using Newtonsoft.Json;
using RemarkableSolutions.Anticaptcha.Responses.Abstractions;

namespace RemarkableSolutions.Anticaptcha.Responses
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