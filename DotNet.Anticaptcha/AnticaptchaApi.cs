using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DotNet.Anticaptcha.Internal.Helpers;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Responses;
using DotNet.Anticaptcha.Responses.Abstractions;

namespace DotNet.Anticaptcha
{
    public static class AnticaptchaApi
    {
        private const string Host = "api.anti-captcha.com";

        public enum ApiMethod
        {
            CreateTask,
            GetTaskResult,
            GetBalance,
            GetQueueStats,
            ReportIncorrectImageCaptcha,
            ReportIncorrectRecaptcha,
            ReportCorrectRecaptcha,
            ReportIncorrectHCaptcha,
            PushAntiGateVariable,
            GetSpendingStats,
            GetAppStats,
        }
        public static CreateTaskResponse CreateTask(JObject jsonPostData)
        {
            return CallApiMethod<CreateTaskResponse>(ApiMethod.CreateTask, jsonPostData);
        }
        
        public static BalanceResponse GetBalance(JObject jsonPostData)
        {
            return CallApiMethod<BalanceResponse>(ApiMethod.GetBalance, jsonPostData);
        }

        public static TaskResultResponse<TSolution> GetTaskResult<TSolution>(JObject payload)
            where TSolution : BaseSolution, new()
        {
            return CallApiMethod<TaskResultResponse<TSolution>>(ApiMethod.GetTaskResult, payload);
        }
        private static T CallApiMethod<T>(ApiMethod methodName, JObject payload) 
            where T : BaseResponse, new()
        {
            return CallApiMethodLogic<T>(true, methodName, payload).Result;
        }
        
        public static async Task<CreateTaskResponse> CreateTaskAsync(JObject payload)
        {
            return await CallApiMethodAsync<CreateTaskResponse>(ApiMethod.CreateTask, payload);
        }

        public static async Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(JObject payload)
            where TSolution : BaseSolution, new()
        {
            return await CallApiMethodAsync<TaskResultResponse<TSolution>>(ApiMethod.GetTaskResult, payload);
        }

        public static async Task<BalanceResponse> GetBalanceAsync(JObject payload)
        {
            return await CallApiMethodAsync<BalanceResponse>(ApiMethod.GetBalance, payload);
        }

        public static async Task<TResponse> CallApiMethodAsync<TResponse>(ApiMethod methodName, JObject payload)
            where TResponse : BaseResponse, new()
        {
            return await CallApiMethodLogic<TResponse>(true, methodName, payload);
        }

        private static async Task<T> CallApiMethodLogic<T>(bool isAsync, ApiMethod methodName, JObject payload) 
            where T : BaseResponse, new()
        {
            var uri = CreateAntiCaptchaUri(methodName);
            var serializedPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);
            return isAsync ? await HttpHelper.PostAsync<T>(uri, serializedPayload) : HttpHelper.Post<T>(uri, serializedPayload);
        }

        private static Uri CreateAntiCaptchaUri(ApiMethod methodName)
        {
            var methodNameStr = char.ToLowerInvariant(methodName.ToString()[0]) + methodName.ToString().Substring(1);
            return new Uri("https://" + Host + "/" + methodNameStr);
        }

    }
}