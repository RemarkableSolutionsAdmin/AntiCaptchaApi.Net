﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Internal.Helpers;
using AntiCaptchaApi.Net.Internal.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using Newtonsoft.Json.Linq;

namespace AntiCaptchaApi.Net.Internal.Services
{
    internal class AnticaptchaApi : IAnticaptchaApi
    {
        private const string Host = "api.anti-captcha.com";

        public async Task<CreateTaskResponse> CreateTaskAsync<TPayload>(TPayload payload, CancellationToken cancellationToken)
            where TPayload : Payload<CreateTaskResponse>
        {
            return await CallApiMethodAsync(ApiMethod.CreateTask, payload, cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(GetTaskPayload<TSolution> payload, CancellationToken cancellationToken)
            where TSolution : BaseSolution, new()
        {
            return await CallApiMethodAsync(ApiMethod.GetTaskResult, payload, cancellationToken);
        }

        public async Task<BalanceResponse> GetBalanceAsync<TPayload>(TPayload payload, CancellationToken cancellationToken)
            where TPayload : Payload<BalanceResponse>
        {
            return await CallApiMethodAsync(ApiMethod.GetBalance, payload, cancellationToken);
        }

        public async Task<TResponse> CallApiMethodAsync<TResponse>(ApiMethod methodName, Payload<TResponse> payload, CancellationToken cancellationToken)
            where TResponse : BaseResponse, new()
        {
            var uri = CreateAntiCaptchaUri(methodName);
            var jsonSerializer = JsonSerializerHelper.GetJsonSerializer();
            var serializedPayload = JObject.FromObject(payload, jsonSerializer).ToString();
            return await HttpHelper.PostAsync<TResponse>(uri, serializedPayload, cancellationToken);
        }

        private static Uri CreateAntiCaptchaUri(ApiMethod methodName)
        {
            var methodNameStr = char.ToLowerInvariant(methodName.ToString()[0]) + methodName.ToString().Substring(1);
            return new Uri("https://" + Host + "/" + methodNameStr);
        }

    }
}