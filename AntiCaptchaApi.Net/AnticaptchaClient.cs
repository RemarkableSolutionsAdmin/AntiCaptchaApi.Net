using System;
using System.Linq;
using System.Net;
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
        public string ClientKey => _postRequestPayloadBuilder.ClientKey;

        public AnticaptchaClient(string clientKey)
        {
            _postRequestPayloadBuilder = new PostRequestPayloadBuilder(clientKey);
        }
        public async Task<GetQueueStatsResponse> GetQueueStatsAsync(QueueType queueType)
        {
            var payload = _postRequestPayloadBuilder.BuildGetQueueStatsPayload(queueType);
            return await AnticaptchaApi.CallApiMethodAsync<GetQueueStatsResponse>(AnticaptchaApi.ApiMethod.GetQueueStats, payload);
        }

        public async Task<GetAppStatsResponse> GetAppStatsAsync(int softId, AppStatsMode? mode = null)
        {
            var payload = _postRequestPayloadBuilder.BuildGetAppStatsPayload(softId, mode);
            return await AnticaptchaApi.CallApiMethodAsync<GetAppStatsResponse>(AnticaptchaApi.ApiMethod.GetAppStats, payload);
        }

        public async Task<GetSpendingStatsResponse> GetSpendingStatsAsync(int date, string queue, int? softId = null, string ip = null)
        {
            var payload = _postRequestPayloadBuilder.BuildGetSpendingStatsPayload(date, queue, softId, ip);
            return await AnticaptchaApi.CallApiMethodAsync<GetSpendingStatsResponse>(AnticaptchaApi.ApiMethod.GetSpendingStats, payload);
        }

        public async Task<ActionResponse> PushAntiGateVariableAsync(int taskId, string name, object value)
        {
            var payload = _postRequestPayloadBuilder.BuildPushAntiGateVariablePayload(taskId, name, value);
            return await AnticaptchaApi.CallApiMethodAsync<ActionResponse>(AnticaptchaApi.ApiMethod.PushAntiGateVariable, payload);
        }

        public async Task<ActionResponse> ReportIncorrectImageCaptchaAsync(int taskId) =>
            await ReportCaptcha(taskId, AnticaptchaApi.ApiMethod.ReportIncorrectImageCaptcha);

        public async Task<ActionResponse> ReportIncorrectImageRecaptchaAsync(int taskId) =>
            await ReportCaptcha(taskId, AnticaptchaApi.ApiMethod.ReportIncorrectRecaptcha);


        public async Task<ActionResponse> ReportCorrectRecaptchaAsync(int taskId) =>
            await ReportCaptcha(taskId, AnticaptchaApi.ApiMethod.ReportCorrectRecaptcha);


        public async Task<ActionResponse> ReportIncorrectImageHCaptchaAsync(int taskId) =>
            await ReportCaptcha(taskId, AnticaptchaApi.ApiMethod.ReportIncorrectHCaptcha);


        private async Task<ActionResponse> ReportCaptcha(int taskId, AnticaptchaApi.ApiMethod method)
        {
            var payload = _postRequestPayloadBuilder.BuildGetTaskPayload(taskId);
            return await AnticaptchaApi.CallApiMethodAsync<ActionResponse>(method, payload);
        }

        public async Task<BalanceResponse> GetBalanceAsync()
        {
            return await GetBalanceLogicAsync();
        }

        public async Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(int taskId)
            where TSolution : BaseSolution, new()
        {
            return await GetCurrentTaskResultAsync<TSolution>(taskId);
        }

        public async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(CaptchaRequest<TSolution> request, int maxSeconds = 120, int currentSecond = 0)
            where TSolution : BaseSolution, new()
        {
            var createTaskResponse = await CreateCaptchaTaskLogic(request);
            if (!createTaskResponse.IsErrorResponse && createTaskResponse.TaskId.HasValue)
            {
                var taskResult = await WaitForTaskResultAsync<TSolution>(createTaskResponse.TaskId.Value, maxSeconds, currentSecond);
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

        
        public async Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(int taskId, int maxSeconds = 120)
            where TSolution : BaseSolution, new()
        {
            return await WaitForTaskResultAsync<TSolution>(taskId, maxSeconds, 0);
        }


        public async Task<CreateTaskResponse> CreateCaptchaTaskAsync<T>(CaptchaRequest<T> request) where T : BaseSolution
        {
            return await CreateCaptchaTaskLogic(request);
        }

        private async Task<BalanceResponse> GetBalanceLogicAsync()
        {
            var payload = _postRequestPayloadBuilder.BuildBasePayload();
            return await AnticaptchaApi.GetBalanceAsync(payload);
        }

        private async Task<TaskResultResponse<TSolution>> GetCurrentTaskResultAsync<TSolution>(int taskId)
            where TSolution : BaseSolution, new()
        {
            var payload = _postRequestPayloadBuilder.BuildGetTaskPayload(taskId);
            return await AnticaptchaApi.GetTaskResultAsync<TSolution>(payload);
        }

        private async Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(int taskId, int maxSeconds, int currentSecond)
            where TSolution : BaseSolution, new()
        {
            if (currentSecond >= maxSeconds)
            {
                return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.RequestTimeout.ToString(), ErrorMessages.AnticaptchaTimeoutError);
            }

            await Waiter.Wait(currentSecond);

            var taskResult = await GetCurrentTaskResultAsync<TSolution>(taskId);

            switch (taskResult.Status)
            {
                case TaskStatusType.Processing:
                        return await WaitForTaskResultAsync<TSolution>(taskId, maxSeconds, currentSecond + 1);
                case TaskStatusType.Ready:
                    return taskResult;
                case TaskStatusType.Error:
                    return taskResult;
                case null:
                    if (string.IsNullOrEmpty(taskResult.ErrorCode) && string.IsNullOrEmpty(taskResult.ErrorDescription))
                        return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.InternalServerError.ToString(), ErrorMessages.AnticaptchaUnknownStatusError);
                    else
                        return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode, taskResult.ErrorDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<CreateTaskResponse> CreateCaptchaTaskLogic<T>(CaptchaRequest<T> request) 
            where T : BaseSolution
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), string.Join('\n', validationResult.Errors.Select(x => x.ToString())));

            var requestPayload = request.ToPayload();

            if (requestPayload == null)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), ErrorMessages.AnticaptchaPayloadBuildValidationFailedError);

            var payload = _postRequestPayloadBuilder.BuildTaskCreationPayload(requestPayload);
            return await AnticaptchaApi.CreateTaskAsync(payload);
        }
    }
}