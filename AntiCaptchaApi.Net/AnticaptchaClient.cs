using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Internal;
using AntiCaptchaApi.Net.Internal.Common;
using AntiCaptchaApi.Net.Internal.Extensions;
using AntiCaptchaApi.Net.Internal.Helpers;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;

namespace AntiCaptchaApi.Net
{
    public class AnticaptchaClient
    {
        private readonly PostRequestPayloadBuilder _postRequestPayloadBuilder;

        public AnticaptchaClient(string clientKey)
        {
            _postRequestPayloadBuilder = new PostRequestPayloadBuilder(clientKey);
        }
        public async Task<GetQueueStatsResponse> GetQueueStatsAsync(QueueType queueType, CancellationToken cancellationToken = default)
        {
            var payload = _postRequestPayloadBuilder.BuildGetQueueStatsPayload(queueType);
            return await AnticaptchaApi.CallApiMethodAsync<GetQueueStatsResponse>(ApiMethod.GetQueueStats, payload, cancellationToken);
        }

        public async Task<GetAppStatsResponse> GetAppStatsAsync(int softId, AppStatsMode? mode = null, CancellationToken cancellationToken = default)
        {
            var payload = _postRequestPayloadBuilder.BuildGetAppStatsPayload(softId, mode);
            return await AnticaptchaApi.CallApiMethodAsync<GetAppStatsResponse>(ApiMethod.GetAppStats, payload, cancellationToken);
        }

        public async Task<GetSpendingStatsResponse> GetSpendingStatsAsync(int date, string queue, int? softId = null, string ip = null, CancellationToken cancellationToken = default)
        {
            var payload = _postRequestPayloadBuilder.BuildGetSpendingStatsPayload(date, queue, softId, ip);
            return await AnticaptchaApi.CallApiMethodAsync<GetSpendingStatsResponse>(ApiMethod.GetSpendingStats, payload, cancellationToken);
        }

        public async Task<ActionResponse> PushAntiGateVariableAsync(int taskId, string name, object value, CancellationToken cancellationToken = default)
        {
            var payload = _postRequestPayloadBuilder.BuildPushAntiGateVariablePayload(taskId, name, value);
            return await AnticaptchaApi.CallApiMethodAsync<ActionResponse>(ApiMethod.PushAntiGateVariable, payload, cancellationToken);
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
            var payload = _postRequestPayloadBuilder.BuildGetTaskPayload(taskId);
            return await AnticaptchaApi.CallApiMethodAsync<ActionResponse>(method, payload, cancellationToken);
        }

        public async Task<BalanceResponse> GetBalanceAsync(CancellationToken cancellationToken = default)
        {
            return await GetBalanceLogicAsync(cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(int taskId, CancellationToken cancellationToken = default)
            where TSolution : BaseSolution, new()
        {
            return await GetCurrentTaskResultAsync<TSolution>(taskId, cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(
            CaptchaRequest<TSolution> request, int maxSeconds = 120, CancellationToken cancellationToken = default)
            where TSolution : BaseSolution, new()
        {
            var createTaskResponse = await CreateCaptchaTaskLogic(request, cancellationToken);
            if (!createTaskResponse.IsErrorResponse && createTaskResponse.TaskId.HasValue)
            {
                var taskResult = await WaitForTaskResultAsync<TSolution>(createTaskResponse.TaskId.Value, maxSeconds, cancellationToken);
                taskResult.CreateTaskResponse = createTaskResponse; //TODO should be done in serializer.
                taskResult.CaptchaRequest = request;
                return taskResult;
            }

            return new TaskResultResponse<TSolution>
            {
                CreateTaskResponse = createTaskResponse,
                ErrorDescription = createTaskResponse.ErrorDescription,
                ErrorCode = createTaskResponse.ErrorCode,
                ErrorId = createTaskResponse.ErrorId
            };
        }

        public async Task<CreateTaskResponse> CreateCaptchaTaskAsync<T>(CaptchaRequest<T> request, CancellationToken cancellationToken = default) 
            where T : BaseSolution
        {
            return await CreateCaptchaTaskLogic(request, cancellationToken);
        }

        private async Task<BalanceResponse> GetBalanceLogicAsync(CancellationToken cancellationToken)
        {
            var payload = _postRequestPayloadBuilder.BuildBasePayload();
            return await AnticaptchaApi.GetBalanceAsync(payload, cancellationToken);
        }

        private async Task<TaskResultResponse<TSolution>> GetCurrentTaskResultAsync<TSolution>(int taskId,
            CancellationToken cancellationToken)
            where TSolution : BaseSolution, new()
        {
            var payload = _postRequestPayloadBuilder.BuildGetTaskPayload(taskId);
            return await AnticaptchaApi.GetTaskResultAsync<TSolution>(payload, cancellationToken);
        }

        public async Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(int taskId, int maxSeconds = 120, CancellationToken cancellationToken = default) 
            where TSolution : BaseSolution, new()
        {
            var currentSecond = 0;
            while (true)
            {
                if (currentSecond >= maxSeconds)
                {
                    return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.RequestTimeout.ToString(), ErrorMessages.AnticaptchaTimeoutError);
                }

                await Waiter.Wait(currentSecond);

                var taskResult = await GetCurrentTaskResultAsync<TSolution>(taskId, cancellationToken);

                switch (taskResult.Status)
                {
                    case TaskStatusType.Processing:
                        currentSecond += 1;
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

        private async Task<CreateTaskResponse> CreateCaptchaTaskLogic<T>(CaptchaRequest<T> request, CancellationToken cancellationToken) 
            where T : BaseSolution
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), string.Join('\n', validationResult.Errors.Select(x => x.ToString())));

            var requestPayload = request.ToPayload();

            if (requestPayload == null)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), ErrorMessages.AnticaptchaPayloadBuildValidationFailedError);

            var payload = _postRequestPayloadBuilder.BuildTaskCreationPayload(requestPayload);
            return await AnticaptchaApi.CreateTaskAsync(payload, cancellationToken);
        }
    }
}