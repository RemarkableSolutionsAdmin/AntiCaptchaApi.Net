namespace AntiCaptchaApi.Net.Internal.Common
{
    internal static class ErrorMessages
    {
        internal const string AnticaptchaNoSolutionFromAPIError = "No solution from API";
        internal const string AnticaptchaApiError = "Anticaptcha API error";
        internal const string AnticaptchaPayloadBuildValidationFailedError = "Anticaptcha request payload creation failed.";
        internal const string AnticaptchaClientKeyValidationError = "Client not found in task manager configured or request!";
        internal const string AnticaptchaTimeoutError = "Anticaptcha task timeout";
        internal const string AnticaptchaUnknownStatusError = "An unknown API status, please update your software";
        internal static string AnticaptchaApiSpecificError (string errorCode, string errorDescription) => $"API Error {errorCode} : {errorDescription}";
    }
}