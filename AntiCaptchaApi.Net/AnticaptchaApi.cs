using System;
using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
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

        public static async Task<CreateTaskResponse> CreateTaskAsync(JObject payload, CancellationToken cancellationToken)
        {
            return await CallApiMethodAsync<CreateTaskResponse>(ApiMethod.CreateTask, payload, cancellationToken);
        }

        public static async Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(JObject payload, CancellationToken cancellationToken)
            where TSolution : BaseSolution, new()
        {
            return await CallApiMethodAsync<TaskResultResponse<TSolution>>(ApiMethod.GetTaskResult, payload, cancellationToken);
        }

        public static async Task<BalanceResponse> GetBalanceAsync(JObject payload, CancellationToken cancellationToken)
        {
            return await CallApiMethodAsync<BalanceResponse>(ApiMethod.GetBalance, payload, cancellationToken);
        }

        public static async Task<TResponse> CallApiMethodAsync<TResponse>(ApiMethod methodName, JObject payload, CancellationToken cancellationToken)
            where TResponse : BaseResponse, new()
        {
            var uri = CreateAntiCaptchaUri(methodName);
            var serializedPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);
            return await HttpHelper.PostAsync<TResponse>(uri, serializedPayload, cancellationToken);
        }

        private static Uri CreateAntiCaptchaUri(ApiMethod methodName)
        {
            var methodNameStr = char.ToLowerInvariant(methodName.ToString()[0]) + methodName.ToString().Substring(1);
            return new Uri("https://" + Host + "/" + methodNameStr);
        }

    }
}