using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Internal;
using AntiCaptchaApi.Net.Internal.Common;
using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Helpers;
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

        public AnticaptchaClient(string clientKey, ClientConfig clientConfig = null)
        {
            ClientConfig = clientConfig ?? new ClientConfig();
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
            ICaptchaRequest<TSolution> request, string languagePool = null, string callbackUrl = null,  CancellationToken cancellationToken = default)
            where TSolution : BaseSolution, new()
        {
            var createTaskResponse = new CreateTaskResponse();
            var taskResult = new TaskResultResponse<TSolution>();
            for (var tries = 0; tries < ClientConfig.SolveAsyncMaxRetries; tries++)
            {          
                createTaskResponse = await CreateCaptchaTaskAsync(request, languagePool, callbackUrl, cancellationToken);
                if (!createTaskResponse.IsErrorResponse && createTaskResponse.TaskId.HasValue)
                {
                    taskResult = await WaitForTaskResultAsync<TSolution>(createTaskResponse.TaskId.Value, cancellationToken);
                    taskResult.CreateTaskResponse = createTaskResponse; //TODO should be done in serializer.
                    taskResult.CaptchaRequest = request;
                    
                    if (taskResult.Status != TaskStatusType.Error)
                    {
                        return taskResult;   
                    }
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
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), string.Join('\n', validationResult.Errors.Select(x => x.ToString())));

            var taskPayload = request.ToPayload();

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

        public async Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(int taskId, CancellationToken cancellationToken = default) 
            where TSolution : BaseSolution, new()
        {
            var timer = new Stopwatch();
            timer.Start();

            TaskResultResponse<TSolution> taskResult = null;
            while (true)
            {
                if (timer.Elapsed.TotalSeconds >= ClientConfig.MaxWaitingTimeInSeconds)
                {
                    if (taskResult == null)
                    {
                        return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.RequestTimeout.ToString(), ErrorMessages.AnticaptchaTimeoutError);
                    }
                    taskResult.ErrorDescription ??= ErrorMessages.AnticaptchaTimeoutError;
                    taskResult.ErrorCode ??= HttpStatusCode.RequestTimeout.ToString();
                    return taskResult;

                }

                await Task.Delay(ClientConfig.StepWaitingTimeInMilliseconds, cancellationToken);
                taskResult = await GetTaskResultAsync<TSolution>(taskId, cancellationToken);

                switch (taskResult.Status)
                {
                    case TaskStatusType.Processing:
                        continue;
                    case TaskStatusType.Ready:
                        return taskResult;
                    case TaskStatusType.Error:
                        return taskResult;
                    case null:
                        if (string.IsNullOrEmpty(taskResult.ErrorCode) && string.IsNullOrEmpty(taskResult.ErrorDescription))
                            return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.InternalServerError.ToString(), ErrorMessages.AnticaptchaUnknownStatusError);
                        return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode, taskResult.ErrorDescription);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}