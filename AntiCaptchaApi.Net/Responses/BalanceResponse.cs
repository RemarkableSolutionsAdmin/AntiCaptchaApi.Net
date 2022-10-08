using AntiCaptchaApi.Responses.Abstractions;

namespace AntiCaptchaApi.Responses
{
    public sealed class BalanceResponse : BaseResponse
    {
        public double? Balance { get; set; }

        public BalanceResponse() { }
        public BalanceResponse(string errorCode, string errorDescription) : base(errorCode, errorDescription) {}
    }
}