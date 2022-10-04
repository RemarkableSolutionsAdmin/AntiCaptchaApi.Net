namespace RemarkableSolutions.Anticaptcha.Responses.Abstractions
{
    public abstract class BaseResponse
    {
        public int? ErrorId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription {  get; set; }
        
        public string RawResponse { get; set; }

        protected BaseResponse()
        {
            
        }

        protected BaseResponse(string errorCode, string errorDescription)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
        
        public bool HasNoErrors => string.IsNullOrEmpty(ErrorDescription) && 
                                   string.IsNullOrEmpty(ErrorCode) &&
                                   ErrorId is null or 0;
    }
}