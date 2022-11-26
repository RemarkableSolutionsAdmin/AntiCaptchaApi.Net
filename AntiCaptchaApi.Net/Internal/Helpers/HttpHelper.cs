using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Internal.Converters;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Internal.Helpers
{
    internal static class HttpHelper
    {
        private static readonly List<JsonConverter> Converters = new();
        static HttpHelper()
        {
            Converters.AddRange(new JsonConverter[]{ 
                new TaskResultConverter<FunCaptchaSolution>(),
                new AntiGateTaskResultConverter(),
                new TaskResultConverter<GeeTestV3Solution>(),
                new TaskResultConverter<GeeTestV4Solution>(),
                new TaskResultConverter<HCaptchaSolution>(),
                new TaskResultConverter<RecaptchaSolution>(),
                new TaskResultConverter<ImageToTextSolution>(),
            });
        }

        internal static async Task<T> PostAsync<T>(Uri url, string payload)
            where T : BaseResponse, new()
        {
            var response = new T();
            var payloadBody = Encoding.UTF8.GetBytes(payload);
            var request = CreatePostRequest(url, payload);
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    await stream.WriteAsync(payloadBody, 0, payloadBody.Length);
                    stream.Close();
                }
                
                using (var webResponse = await request.GetResponseAsync())
                {
                    var streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                    var rawResponse = await streamReader.ReadToEndAsync();
                    response = JsonConvert.DeserializeObject<T>(rawResponse, Converters.ToArray());
                    response.RawResponse = rawResponse;
                    response.RawRequestPayload = payload;
                    webResponse.Close();
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.ErrorDescription = ex.Message;
                return response;
            }
        }
        
        private static HttpWebRequest CreatePostRequest(Uri url, string post)
        {
            var postBody = Encoding.UTF8.GetBytes(post);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = postBody.Length;
            request.Timeout = 30000;
            return request;
        }
    }
}