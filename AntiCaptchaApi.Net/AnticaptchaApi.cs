using System;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Internal.Helpers;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net
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
            var uri = CreateAntiCaptchaUri(methodName);
            var serializedPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);
            return await HttpHelper.PostAsync<TResponse>(uri, serializedPayload);
        }

        private static Uri CreateAntiCaptchaUri(ApiMethod methodName)
        {
            var methodNameStr = char.ToLowerInvariant(methodName.ToString()[0]) + methodName.ToString().Substring(1);
            return new Uri("https://" + Host + "/" + methodNameStr);
        }

    }
}