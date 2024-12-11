using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Internal;
using AntiCaptchaApi.Net.Internal.Common;
using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Helpers;
using AntiCaptchaApi.Net.Internal.Models;
using AntiCaptchaApi.Net.Internal.Services;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using AntiCaptchaApi.Net.Responses;

namespace AntiCaptchaApi.Net
{
    public class AnticaptchaClient : IAnticaptchaClient
    {
        public ClientConfig ClientConfig { get; private set; }

        public string ClientKey { get; }
        
        private IAnticaptchaApi AnticaptchaApi { get; set; }

        public AnticaptchaClient(string clientKey, ClientConfig clientConfig = null, HttpClient httpClient = null)
        {
            ClientConfig = clientConfig ?? new ClientConfig();
            httpClient ??= new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(ClientConfig.MaxHttpRequestTimeMs)
            };
            AnticaptchaApi = new AnticaptchaApi(httpClient);
            ClientKey = clientKey;
        }

        internal AnticaptchaClient(
            IAnticaptchaApi anticaptchaApi,
            string clientKey,
            ClientConfig clientConfig = null)
        {
            ClientConfig = clientConfig ?? new ClientConfig();
            AnticaptchaApi = anticaptchaApi;
            ClientKey = clientKey;
        }

        public void Configure(ClientConfig clientConfig)
        {
            ClientConfig = clientConfig ?? new ClientConfig();;
        }
        
        public async Task<GetQueueStatsResponse> GetQueueStatsAsync(QueueType queueType, CancellationToken cancellationToken = default)
        {
            var payload = new GetQueueStatsPayload((int)queueType);
            return await AnticaptchaApi.CallApiMethodAsync(ApiMethod.GetQueueStats, payload, cancellationToken);
        }

        public async Task<GetAppStatsResponse> GetAppStatsAsync(int softId, AppStatsMode? mode = null, CancellationToken cancellationToken = default)
        {
            var payload = new GetAppStatsPayload(ClientKey, softId, mode?.ToString());
            return await AnticaptchaApi.CallApiMethodAsync(ApiMethod.GetAppStats, payload, cancellationToken);
        }

        public async Task<GetSpendingStatsResponse> GetSpendingStatsAsync(int? date = null, string queue = null, int? softId = null, string ip = null, CancellationToken cancellationToken = default)
        {
            var payload = new GetSpendingStatsPayload(ClientKey, queue, softId, date, ip);
            return await AnticaptchaApi.CallApiMethodAsync(ApiMethod.GetSpendingStats, payload, cancellationToken);
        }

        public async Task<ActionResponse> PushAntiGateVariableAsync(int taskId, string name, object value, CancellationToken cancellationToken = default)
        { 
            var payload = new PushAntiGateVariablePayload(ClientKey, taskId, name, value);
            return await AnticaptchaApi.CallApiMethodAsync(ApiMethod.PushAntiGateVariable, payload, cancellationToken);
        }

        public async Task<ActionResponse> ReportIncorrectImageCaptchaAsync(int taskId, CancellationToken cancellationToken = default) =>
            await ReportCaptcha(taskId, ApiMethod.ReportIncorrectImageCaptcha, cancellationToken);

        public async Task<ActionResponse> ReportIncorrectImageRecaptchaAsync(int taskId, CancellationToken cancellationToken = default) =>
            await ReportCaptcha(taskId, ApiMethod.ReportIncorrectRecaptcha, cancellationToken);


        public async Task<ActionResponse> ReportCorrectRecaptchaAsync(int taskId, CancellationToken cancellationToken = default) =>
            await ReportCaptcha(taskId, ApiMethod.ReportCorrectRecaptcha, cancellationToken);


        public async Task<ActionResponse> ReportIncorrectImageHCaptchaAsync(int taskId, CancellationToken cancellationToken = default) =>
            await ReportCaptcha(taskId, ApiMethod.ReportIncorrectHCaptcha, cancellationToken);


        private async Task<ActionResponse> ReportCaptcha(int taskId, ApiMethod method, CancellationToken cancellationToken)
        {
            var payload = new ActionPayload(ClientKey, taskId);
            return await AnticaptchaApi.CallApiMethodAsync(method, payload, cancellationToken);
        }

        public async Task<BalanceResponse> GetBalanceAsync(CancellationToken cancellationToken = default)
        {
            var payload = new GetBalancePayload(ClientKey);
            return await AnticaptchaApi.GetBalanceAsync(payload, cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(
            ICaptchaRequest<TSolution> request, 
            string languagePool = null,
            string callbackUrl = null,  
            CancellationToken cancellationToken = default)
            where TSolution : BaseSolution, new()
        {
            var createTaskResponse = new CreateTaskResponse();
            var taskResult = new TaskResultResponse<TSolution>();
            for (var tries = 0; tries < ClientConfig.SolveAsyncRetries; ++tries)
            {
                createTaskResponse = await CreateCaptchaTaskAsync<TSolution>(request, languagePool, callbackUrl, cancellationToken);
                if (!createTaskResponse.IsErrorResponse && createTaskResponse.TaskId.HasValue)
                {
                    taskResult = await WaitForTaskResultAsync<TSolution>(createTaskResponse, cancellationToken);
                    taskResult.CaptchaRequest = request;
                    var status = taskResult.Status;
                    var taskStatusType = TaskStatusType.Error;
                    if (!(status.GetValueOrDefault() == taskStatusType & status.HasValue))
                        return taskResult;
                }
            }
            taskResult.CreateTaskResponse = createTaskResponse;
            if (!taskResult.CreateTaskResponse.IsErrorResponse)
                return taskResult;
            taskResult.ErrorDescription = createTaskResponse.ErrorDescription;
            taskResult.ErrorCode = createTaskResponse.ErrorCode;
            taskResult.ErrorId = createTaskResponse.ErrorId;
            return taskResult;
        }

        public async Task<CreateTaskResponse> CreateCaptchaTaskAsync<T>(ICaptchaRequest<T> request, string languagePool = null, string callbackUrl = null, CancellationToken cancellationToken = default) 
            where T : BaseSolution
        {
            var validationResult = CreateCaptchaRequestHelper.Validate(request);
            if (!validationResult.IsValid)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), string.Join('\n', validationResult.Errors.Select(x => x.ToString())));

            var taskPayload = CreateCaptchaRequestHelper.Build(request);

            if (taskPayload == null)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), ErrorMessages.AnticaptchaPayloadBuildValidationFailedError);

            var payload = new CreateTaskPayload(ClientKey, taskPayload, languagePool, callbackUrl);
            return await AnticaptchaApi.CreateTaskAsync(payload, cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(int taskId, CancellationToken cancellationToken)
            where TSolution : BaseSolution, new()
        {
            var payload = new GetTaskPayload<TSolution>(ClientKey, taskId);
            return await AnticaptchaApi.GetTaskResultAsync(payload, cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(int taskId, CancellationToken cancellationToken = default(CancellationToken)) where TSolution : BaseSolution, new()
        {
            var taskResultResponse = await WaitForTaskResultAsync<TSolution>(new CreateTaskResponse()
            {
                TaskId = taskId
            }, cancellationToken);
            return taskResultResponse;
        }

        public async Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(
            CreateTaskResponse createTaskResponse,
            CancellationToken cancellationToken = default) 
            where TSolution : BaseSolution, new()
        {
            if (!createTaskResponse.TaskId.HasValue)
                throw new ArgumentNullException(nameof(createTaskResponse.TaskId));
            var timer = new Stopwatch();
            timer.Start();
            var taskResultResponse = new TaskResultResponse<TSolution>()
            {
                CreateTaskResponse = createTaskResponse
            };

            while (timer.ElapsedMilliseconds <= ClientConfig.MaxWaitForTaskResultTimeMs)
            {
                await Task.Delay(ClientConfig.DelayTimeBetweenCheckingTaskResultMs, cancellationToken);
                taskResultResponse = await GetTaskResultAsync<TSolution>(createTaskResponse.TaskId.Value, cancellationToken);
                taskResultResponse.CreateTaskResponse = createTaskResponse;
                var status = taskResultResponse.Status;
                if (!status.HasValue)
                    return !string.IsNullOrEmpty(taskResultResponse.ErrorCode) || !string.IsNullOrEmpty(taskResultResponse.ErrorDescription) ? BaseTaskResultResponseBuilder.Build<TSolution>(taskResultResponse.ErrorCode, taskResultResponse.ErrorDescription) : BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.InternalServerError.ToString(), "An unknown API status, please contact code owners to fix this issue.");
                var valueOrDefault = status.GetValueOrDefault();
                switch (valueOrDefault)
                {
                    case TaskStatusType.Processing:
                        continue;
                    case TaskStatusType.Ready:
                    case TaskStatusType.Error:
                        return taskResultResponse;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            var taskResultResponse1 = taskResultResponse;
            if (taskResultResponse1.ErrorDescription == null)
            {
                var taskId2 = (ValueType) createTaskResponse.TaskId;
                var totalMilliseconds = (ValueType) timer.Elapsed.TotalMilliseconds;
                var str2 = $"Anticaptcha task did not finish in given time limit. The task id: {(object)taskId2}, the time elapsed: {(object)totalMilliseconds} ms.";
                taskResultResponse1.ErrorDescription = str2;
            }
            var taskResultResponse3 = taskResultResponse;
            if (taskResultResponse3.ErrorCode != null) return taskResultResponse;
            const HttpStatusCode httpStatusCode = HttpStatusCode.RequestTimeout;
            var str3 = httpStatusCode.ToString();
            taskResultResponse3.ErrorCode = str3;
            return taskResultResponse;
        }
    }
}