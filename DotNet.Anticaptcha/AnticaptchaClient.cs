using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNet.Anticaptcha.Enums;
using DotNet.Anticaptcha.Internal;
using DotNet.Anticaptcha.Internal.Common;
using DotNet.Anticaptcha.Internal.Extensions;
using DotNet.Anticaptcha.Internal.Helpers;
using DotNet.Anticaptcha.Models.Solutions;
using DotNet.Anticaptcha.Requests.Abstractions;
using DotNet.Anticaptcha.Responses;

namespace DotNet.Anticaptcha
{
    public class AnticaptchaClient
    {
        private readonly PostRequestPayloadBuilder _postRequestPayloadBuilder;
        public string ClientKey => _postRequestPayloadBuilder.ClientKey;

        public AnticaptchaClient(string clientKey)
        {
            _postRequestPayloadBuilder = new PostRequestPayloadBuilder(clientKey);
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
                var solution = taskResult.Solution;
                solution.CreateTaskResponse = createTaskResponse;
                return taskResult;   
            }

            return new TaskResultResponse<TSolution>()
            {
                Solution = { CreateTaskResponse = createTaskResponse }
            };
        }
        
        private async Task<TaskResultResponse<TSolution>> WaitForTaskResultLogic<TSolution>(bool isAsync, int taskId, int maxSeconds, int currentSecond)
            where TSolution : BaseSolution, new()
        {
            if (currentSecond >= maxSeconds)
            {
                return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.RequestTimeout.ToString(),  ErrorMessages.AnticaptchaTimeoutError);
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
                    if(string.IsNullOrEmpty(taskResult.ErrorCode) && string.IsNullOrEmpty(taskResult.ErrorDescription))
                        return BaseTaskResultResponseBuilder.Build<TSolution>(HttpStatusCode.InternalServerError.ToString(),  ErrorMessages.AnticaptchaUnknownStatusError);
                    else
                        return BaseTaskResultResponseBuilder.Build<TSolution>(taskResult.ErrorCode,  taskResult.ErrorDescription);
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