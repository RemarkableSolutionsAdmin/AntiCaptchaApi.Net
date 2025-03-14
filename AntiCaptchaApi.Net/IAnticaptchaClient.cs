﻿using System.Threading;
using System.Threading.Tasks;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using AntiCaptchaApi.Net.Responses;

namespace AntiCaptchaApi.Net;

public interface IAnticaptchaClient
{
    ClientConfig ClientConfig { get; }

    void Configure(ClientConfig clientConfig);

    Task<GetQueueStatsResponse> GetQueueStatsAsync(
      QueueType queueType,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<GetAppStatsResponse> GetAppStatsAsync(
      int softId,
      AppStatsMode? mode = null,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<GetSpendingStatsResponse> GetSpendingStatsAsync(
      int? date = null,
      string queue = null,
      int? softId = null,
      string ip = null,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<ActionResponse> PushAntiGateVariableAsync(
      int taskId,
      string name,
      object value,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<ActionResponse> ReportIncorrectImageCaptchaAsync(
      int taskId,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<ActionResponse> ReportIncorrectImageRecaptchaAsync(
      int taskId,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<ActionResponse> ReportCorrectRecaptchaAsync(
      int taskId,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<ActionResponse> ReportIncorrectImageHCaptchaAsync(
      int taskId,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<BalanceResponse> GetBalanceAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(
      ICaptchaRequest<TSolution> request,
      string languagePool = null,
      string callbackUrl = null,
      CancellationToken cancellationToken = default (CancellationToken))
      where TSolution : BaseSolution, new();

    Task<CreateTaskResponse> CreateCaptchaTaskAsync<T>(
      ICaptchaRequest<T> request,
      string languagePool = null,
      string callbackUrl = null,
      CancellationToken cancellationToken = default (CancellationToken))
      where T : BaseSolution;

    Task<TaskResultResponse<TSolution>> GetTaskResultAsync<TSolution>(
      int taskId,
      CancellationToken cancellationToken)
      where TSolution : BaseSolution, new();

    Task<TaskResultResponse<TSolution>> WaitForTaskResultAsync<TSolution>(
      int taskId,
      CancellationToken cancellationToken = default (CancellationToken))
      where TSolution : BaseSolution, new();
}