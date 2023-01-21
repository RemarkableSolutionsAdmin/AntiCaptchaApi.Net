using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace AntiCaptchaApi.Net.Internal
{
    public abstract class Payload <TResponse>
        where TResponse : BaseResponse, new()
    {
        protected const int DefaultSoftId = 1023;
    }

    public abstract class SoftAndClientPayload<TResponse> : ClientPayload<TResponse>
        where TResponse : BaseResponse, new()
    {
        protected SoftAndClientPayload(string clientKey, int softId = DefaultSoftId) : base(clientKey)
        {
            SoftId = softId;
        }
        
        public int SoftId { get;  set; }
    }

    public class CreateTaskPayload : SoftAndClientPayload<CreateTaskResponse>
    {
        public CreateTaskPayload(string clientKey, JObject task, string languagePool = null, string callbackUrl = null) : base(clientKey)
        {
            Task = task;
            LanguagePool = languagePool;
            CallbackUrl = callbackUrl;
        }
        public JObject Task { get; }
        
        public string LanguagePool { get; }
        
        public string CallbackUrl { get; }
    }

    public class ClientPayload<TResponse> : Payload <TResponse>
        where TResponse : BaseResponse, new()
    {
        public ClientPayload(string clientKey)
        {
            ClientKey = clientKey;
        }

        public string ClientKey { get; set; }
    }

    public class GetBalancePayload : ClientPayload <BalanceResponse>
    {
        public GetBalancePayload(string clientKey) : base(clientKey)
        {
            
        }
    }

    public class GetSpendingStatsPayload : ClientPayload<GetSpendingStatsResponse>
    {
        public GetSpendingStatsPayload(string clientKey, string queue = null, int? softId = null, int? date  = null, string ip = null) : base(clientKey)
        {
            Date = date;
            Queue = queue;
            SoftId = softId;
            Ip = ip;
        }
        public int? Date { get; set;}
        public string Queue { get; set; }
        public int? SoftId { get; set; }
        public string Ip { get; set; }
    }
    

    public class GetQueueStatsPayload : Payload<GetQueueStatsResponse>
    {
        public GetQueueStatsPayload(int queueId)
        {
            QueueId = queueId;
        }

        public int QueueId { get; }
    }

    public class GetAppStatsPayload : SoftAndClientPayload <GetAppStatsResponse>
    {
        public GetAppStatsPayload(string clientKey, int softId, string mode = null) : base(clientKey, softId)
        {
            Mode = mode;
        }

        public string Mode { get; }
    }

    public class GetTaskPayload <TSolution> : ClientPayload <TaskResultResponse<TSolution>>
        where TSolution : BaseSolution, new()
    {
        public GetTaskPayload(string clientKey, int taskId) : base(clientKey)
        {
            TaskId = taskId;
        }
        
        public int TaskId { get; }
    }
    
    public class ActionPayload : ClientPayload <ActionResponse>
    {
        public ActionPayload(string clientKey, int taskId) : base(clientKey)
        {
            TaskId = taskId;
        }
        
        public int TaskId { get; }
    }

    
    
    public class PushAntiGateVariablePayload : ClientPayload <ActionResponse>
    {
        public PushAntiGateVariablePayload(string clientKey, int taskId, string name, object value) : base(clientKey)
        {
            TaskId = taskId;
            Name = name;
            Value = value;
        }
        public int TaskId { get; }
        public string Name { get; }
        public object Value { get; }
    }
}