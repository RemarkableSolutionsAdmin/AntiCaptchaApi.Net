using Newtonsoft.Json.Linq;

namespace RemarkableSolutions.Anticaptcha.Internal
{
    internal class PostRequestPayloadBuilder
    {
        private const int SoftId = 1023;
        
        internal JObject BuildBasePayload(string clientKey)
        {
            var resultPayload = new JObject();;
            resultPayload.Add("softId", SoftId);
            resultPayload.Add("clientKey", clientKey);
            return resultPayload;
        }
        
        internal JObject BuildTaskCreationPayload(JObject requestPayload, string clientKey)
        {
            var resultPayload = BuildBasePayload(clientKey);
            resultPayload.Add("task", requestPayload);
            return resultPayload;
        }
        
        internal JObject BuildGetTaskPayload(int taskId, string clientKey)
        {
            var resultPayload = BuildBasePayload(clientKey);
            resultPayload.Add("taskId", taskId);
            return resultPayload;
        }
    }
}