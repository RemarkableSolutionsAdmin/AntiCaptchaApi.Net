﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Internal.Converters;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Internal.Helpers
{
    internal class HttpHelper
    {
        private static readonly List<JsonConverter> Converters = new();
        private readonly HttpClient _httpClient;
        
        internal HttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Converters.AddRange(new JsonConverter[]{ 
                new TaskResultConverter<FunCaptchaSolution>(),
                new AntiGateTaskResultConverter(),
                new TaskResultConverter<GeeTestV3Solution>(),
                new TaskResultConverter<GeeTestV4Solution>(),
                new TaskResultConverter<RecaptchaSolution>(),
                new TaskResultConverter<ImageToTextSolution>(),
                new TaskResultConverter<ImageToCoordinatesSolution>(),
                new TaskResultConverter<TurnstileSolution>(),
            });
        }

        internal async Task<T> PostAsync<T>(Uri url, string payload, CancellationToken cancellationToken)
            where T : BaseResponse, new()
        {
            var response = new T();
            var responseContent = string.Empty;
            try
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var httpResponseMessage = await _httpClient.PostAsync(url, (HttpContent) content, cancellationToken);
                responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<T>(responseContent, Converters.ToArray());
                if (response != null)
                {
                    response.RawResponse = responseContent;
                    response.RawPayload = payload;
                }
                return response;
            }
            catch (Exception ex)
            {
                if (response == null)
                {
                    return null;
                }
                response.ErrorDescription = ex.Message; 
                response.RawResponse = responseContent;
                response.RawPayload = payload;
                return response;
            }
        }
    }
}