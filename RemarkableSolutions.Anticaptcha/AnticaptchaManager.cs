using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RemarkableSolutions.Anticaptcha.Enums;
using RemarkableSolutions.Anticaptcha.Internal;
using RemarkableSolutions.Anticaptcha.Internal.Common;
using RemarkableSolutions.Anticaptcha.Internal.Extensions;
using RemarkableSolutions.Anticaptcha.Internal.Helpers;
using RemarkableSolutions.Anticaptcha.Models.Solutions;
using RemarkableSolutions.Anticaptcha.Requests.Abstractions;
using RemarkableSolutions.Anticaptcha.Responses;

namespace RemarkableSolutions.Anticaptcha
{
    public static class AnticaptchaManager
    {
        private static readonly PostRequestPayloadBuilder PostRequestPayloadBuilder = new();

        public static BalanceResponse GetBalance(string clientKey)
        {
            return GetBalanceLogic(false,  clientKey).Result;
        }

        public static async Task<BalanceResponse> GetBalanceAsync(string clientKey)
        {
            return await GetBalanceLogic(true, clientKey);
        }

        public static async Task<TaskResultResponse<TSolution>> GetCurrentRawTaskResultAsync<TSolution>(int taskId, string clientKey)
            where TSolution : BaseSolution, new()
        {
            return await GetCurrentTaskResultLogic<TSolution>(true, taskId, clientKey);
        }

        public static TaskResultResponse<TSolution> GetCurrentRawTaskResult<TSolution>(int taskId, string clientKey)
            where TSolution : BaseSolution, new()
        {
            return GetCurrentTaskResultLogic<TSolution>(false, taskId, clientKey).Result;
        }

        public static TaskResultResponse<TSolution> SolveCaptchaRaw<TRequest, TSolution>(TRequest request)
            where TRequest : CaptchaRequest
            where TSolution : BaseSolution, new()
        {
            return SolveCaptchaLogic<TRequest, TSolution>(false, request).Result;
        }
        
        public static async Task<TaskResultResponse<TSolution>> WaitForTaskRawResultAsync<TSolution>(int taskId, string clientKey, int maxSeconds = 120)
            where TSolution : BaseSolution, new()
        {
            return await WaitForTaskResultLogic<TSolution>(true, taskId, maxSeconds, 0, clientKey);
        }

        public static TaskResultResponse<TSolution> WaitForRawTaskResult<TSolution>(int taskId, string clientKey, int maxSeconds = 120)
            where TSolution : BaseSolution, new()
        {
            return WaitForTaskResultLogic<TSolution>(false, taskId, maxSeconds, 0, clientKey).Result;
        }
        public static CreateTaskResponse CreateCaptchaTask<T>(T request) where T : CaptchaRequest
        {
            return CreateCaptchaTaskLogic(request, false, request.ClientKey).Result;
        }

        public static async Task<CreateTaskResponse> CreateCaptchaTaskAsync<T>(T request) where T : CaptchaRequest
        {
            return await CreateCaptchaTaskLogic(request, true, request.ClientKey);
        }

        private static async Task<BalanceResponse> GetBalanceLogic(bool isAsync, string clientKey)
        {
            var payload = PostRequestPayloadBuilder.BuildBasePayload(clientKey);
            return isAsync ? await AnticaptchaApi.GetBalanceAsync(payload) : AnticaptchaApi.GetBalance(payload);
        }

        private static async Task<TaskResultResponse<TSolution>> GetCurrentTaskResultLogic<TSolution>(bool isAsync, int taskId, string clientKey)
            where TSolution : BaseSolution, new()
        {
            var payload = PostRequestPayloadBuilder.BuildGetTaskPayload(taskId, clientKey);
            return isAsync ? await AnticaptchaApi.GetTaskResultAsync<TSolution>(payload) : AnticaptchaApi.GetTaskResult<TSolution>(payload);
        }

        private static async Task<TaskResultResponse<TSolution>> SolveCaptchaLogic<TRequest, TSolution>(bool isAsync, TRequest request, int maxSeconds = 120, int currentSecond = 0) 
            where TRequest : CaptchaRequest
            where TSolution : BaseSolution, new()
        
        {
            var createTaskResponse = await CreateCaptchaTaskLogic(request, isAsync, request.ClientKey);
            if (createTaskResponse.HasNoErrors && createTaskResponse.TaskId.HasValue)
            {
                var taskResult = await WaitForTaskResultLogic<TSolution>(isAsync, createTaskResponse.TaskId.Value, maxSeconds, currentSecond, request.ClientKey);
                var solution = taskResult.Solution;
                solution.CreateTaskResponse = createTaskResponse;
                return taskResult;   
            }

            return new TaskResultResponse<TSolution>()
            {
                Solution = { CreateTaskResponse = createTaskResponse }
            };
        }
        
        private static async Task<TaskResultResponse<TSolution>> WaitForTaskResultLogic<TSolution>(bool isAsync, int taskId, int maxSeconds, int currentSecond, string clientKey)
            where TSolution : BaseSolution, new()
        {
            if (currentSecond >= maxSeconds)
            {
                return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.RequestTimeout.ToString(),  ErrorMessages.AnticaptchaTimeoutError);
            }

            await Waiter.Wait(isAsync, currentSecond);

            var taskResult = await GetCurrentTaskResultLogic<TSolution>(isAsync, taskId, clientKey);

            switch (taskResult.Status)
            {
                case TaskStatusType.Processing:
                    if (isAsync)
                        return await WaitForTaskResultLogic<TSolution>(isAsync, taskId, maxSeconds, currentSecond + 1, clientKey);
                    return WaitForTaskResultLogic<TSolution>(isAsync, taskId, maxSeconds, currentSecond + 1, clientKey).Result;
                case TaskStatusType.Ready when !taskResult.Solution.IsValid():
                    return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.Conflict.ToString(), ErrorMessages.AnticaptchaNoSolutionFromAPIError);
                case TaskStatusType.Ready:
                    return taskResult;
                case TaskStatusType.Error:
                    return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode, taskResult.ErrorDescription);
                case null:
                    if(string.IsNullOrEmpty(taskResult.ErrorCode) && string.IsNullOrEmpty(taskResult.ErrorDescription))
                        return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.InternalServerError.ToString(),  ErrorMessages.AnticaptchaUnknownStatusError);
                    else
                        return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode,  taskResult.ErrorDescription);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private static async Task<CreateTaskResponse> CreateCaptchaTaskLogic<T>(T request, bool isAsync, string clientKey) where T : CaptchaRequest
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), string.Join('\n', validationResult.Errors.Select(x => x.ToString())));

            var requestPayload = request.ToPayload();
           
            if (requestPayload == null)
               return new CreateTaskResponse(HttpStatusCode.BadRequest.ToString(), ErrorMessages.AnticaptchaPayloadBuildValidationFailedError);

            var payload = PostRequestPayloadBuilder.BuildTaskCreationPayload(requestPayload, clientKey);
            return isAsync ? await AnticaptchaApi.CreateTaskAsync(payload) : AnticaptchaApi.CreateTask(payload);
        }
    }
}