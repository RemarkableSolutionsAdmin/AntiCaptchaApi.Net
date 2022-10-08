using AntiCaptchaApi.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Internal
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
            var payload = new JObject(); ;
            payload.Add("softId", SoftId);
            payload.Add("clientKey", ClientKey);
            return payload;
        }


        internal JObject BuildGetQueueStatsPayload(QueueType type)
        {
            var payload = new JObject(); ;
            payload.Add("queueId", (int)type);
            return payload;
        }
        internal JObject BuildGetSpendingStatsPayload(int date, string queue, int? softId = null, string ip = null)
        {
            var payload = new JObject
            {
                { "clientKey", ClientKey },
                { "date", date },
                { "queue", queue },
            };
            if (softId != null)
            {
                payload.Add("softId", softId);
            }
            if (!string.IsNullOrEmpty(ip))
            {
                payload.Add("ip", ip);
            }

            return payload;
        }

        internal JObject BuildGetAppStatsPayload(int softId, AppStatsMode? mode = null)
        {
            var payload = new JObject
            {
                { "clientKey", ClientKey },
                { "softId", softId },
            };

            if (mode.HasValue)
            {
                payload.Add("mode", mode.ToString());
            }

            return payload;
        }

        internal JObject BuildPushAntiGateVariablePayload(int taskId, string name, object value)
        {
            var payload = BuildGetTaskPayload(taskId);
            payload.Add("name", name);
            payload.Add("value", JsonConvert.SerializeObject(value));
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