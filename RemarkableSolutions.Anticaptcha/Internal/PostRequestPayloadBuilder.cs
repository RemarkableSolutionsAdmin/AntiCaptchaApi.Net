using Newtonsoft.Json.Linq;

namespace RemarkableSolutions.Anticaptcha.Internal
{
    internal class PostRequestPayloadBuilder
    {
        private const int SoftId = 1023;
        public string ClientKey { get; }

        internal PostRequestPayloadBuilder(string clientKey)
        {
            ClientKey = clientKey;
        }

        internal JObject BuildBasePayload()
        {
            var payload = new JObject();;
            payload.Add("softId", SoftId);
            payload.Add("clientKey", ClientKey);
            return payload;
        }
        
        internal JObject BuildTaskCreationPayload(JObject requestPayload)
        {
            var payload = BuildBasePayload();
            payload.Add("task", requestPayload);
            return payload;
        }
        
        internal JObject BuildGetTaskPayload(int taskId)
        {
            var payload = BuildBasePayload();
            payload.Add("taskId", taskId);
            return payload;
        }
    }
}