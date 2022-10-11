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

        public BalanceResponse GetBalance()
        {
            return GetBalanceLogic(false).Result;
        }

        public async Task<BalanceResponse> GetBalanceAsync()
        {
            return await GetBalanceLogic(true);
        }

        public async Task<TaskResultResponse<TSolution>> GetCurrentRawTaskResultAsync<TSolution>(int taskId)
            where TSolution : BaseSolution, new()
        {
            return await GetCurrentTaskResultLogic<TSolution>(true, taskId);
        }

        public TaskResultResponse<TSolution> GetCurrentRawTaskResult<TSolution>(int taskId)
            where TSolution : BaseSolution, new()
        {
            return GetCurrentTaskResultLogic<TSolution>(false, taskId).Result;
        }

        public TaskResultResponse<TSolution> SolveCaptchaRaw<TRequest, TSolution>(TRequest request)
            where TRequest : CaptchaRequest
            where TSolution : BaseSolution, new()
        {
            return SolveCaptchaLogic<TRequest, TSolution>(false, request).Result;
        }

        public async Task<TaskResultResponse<TSolution>> WaitForTaskRawResultAsync<TSolution>(int taskId, int maxSeconds = 120)
            where TSolution : BaseSolution, new()
        {
            return await WaitForTaskResultLogic<TSolution>(true, taskId, maxSeconds, 0);
        }

        public TaskResultResponse<TSolution> WaitForRawTaskResult<TSolution>(int taskId, int maxSeconds = 120)
            where TSolution : BaseSolution, new()
        {
            return WaitForTaskResultLogic<TSolution>(false, taskId, maxSeconds, 0).Result;
        }
        public CreateTaskResponse CreateCaptchaTask<T>(T request) where T : CaptchaRequest
        {
            return CreateCaptchaTaskLogic(request, false).Result;
        }

        public async Task<CreateTaskResponse> CreateCaptchaTaskAsync<T>(T request) where T : CaptchaRequest
        {
            return await CreateCaptchaTaskLogic(request, true);
        }

        private async Task<BalanceResponse> GetBalanceLogic(bool isAsync)
        {
            var payload = _postRequestPayloadBuilder.BuildBasePayload();
            return isAsync ? await AnticaptchaApi.GetBalanceAsync(payload) : AnticaptchaApi.GetBalance(payload);
        }

        private async Task<TaskResultResponse<TSolution>> GetCurrentTaskResultLogic<TSolution>(bool isAsync, int taskId)
            where TSolution : BaseSolution, new()
        {
            var payload = _postRequestPayloadBuilder.BuildGetTaskPayload(taskId);
            return isAsync ? await AnticaptchaApi.GetTaskResultAsync<TSolution>(payload) : AnticaptchaApi.GetTaskResult<TSolution>(payload);
        }

        private async Task<TaskResultResponse<TSolution>> SolveCaptchaLogic<TRequest, TSolution>(bool isAsync, TRequest request, int maxSeconds = 120, int currentSecond = 0)
            where TRequest : CaptchaRequest
            where TSolution : BaseSolution, new()

        {
            var createTaskResponse = await CreateCaptchaTaskLogic(request, isAsync);
            if (createTaskResponse.HasNoErrors && createTaskResponse.TaskId.HasValue)
            {
                var taskResult = await WaitForTaskResultLogic<TSolution>(isAsync, createTaskResponse.TaskId.Value, maxSeconds, currentSecond);
                taskResult.Solution.CreateTaskResponse = createTaskResponse; //TODO should be done in serializer.
                return taskResult;
            }

            return new TaskResultResponse<TSolution>()
            {
                Solution = new TSolution { CreateTaskResponse = createTaskResponse }
            };
        }

        private async Task<TaskResultResponse<TSolution>> WaitForTaskResultLogic<TSolution>(bool isAsync, int taskId, int maxSeconds, int currentSecond)
            where TSolution : BaseSolution, new()
        {
            if (currentSecond >= maxSeconds)
            {
                return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.RequestTimeout.ToString(), ErrorMessages.AnticaptchaTimeoutError);
            }

            await Waiter.Wait(isAsync, currentSecond);

            var taskResult = await GetCurrentTaskResultLogic<TSolution>(isAsync, taskId);

            switch (taskResult.Status)
            {
                case TaskStatusType.Processing:
                    if (isAsync)
                        return await WaitForTaskResultLogic<TSolution>(isAsync, taskId, maxSeconds, currentSecond + 1);
                    return WaitForTaskResultLogic<TSolution>(isAsync, taskId, maxSeconds, currentSecond + 1).Result;
                case TaskStatusType.Ready when !taskResult.Solution.IsValid():
                    return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.Conflict.ToString(), ErrorMessages.AnticaptchaNoSolutionFromAPIError);
                case TaskStatusType.Ready:
                    return taskResult;
                case TaskStatusType.Error:
                    return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode, taskResult.ErrorDescription);
                case null:
                    if (string.IsNullOrEmpty(taskResult.ErrorCode) && string.IsNullOrEmpty(taskResult.ErrorDescription))
                        return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.InternalServerError.ToString(), ErrorMessages.AnticaptchaUnknownStatusError);
                    else
                        return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode, taskResult.ErrorDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<CreateTaskResponse> CreateCaptchaTaskLogic<T>(T request, bool isAsync) where T : CaptchaRequest
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), string.Join('\n', validationResult.Errors.Select(x => x.ToString())));

            var requestPayload = request.ToPayload();

            if (requestPayload == null)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), ErrorMessages.AnticaptchaPayloadBuildValidationFailedError);

            var payload = _postRequestPayloadBuilder.BuildTaskCreationPayload(requestPayload);
            return isAsync ? await AnticaptchaApi.CreateTaskAsync(payload) : AnticaptchaApi.CreateTask(payload);
        }
    }
}