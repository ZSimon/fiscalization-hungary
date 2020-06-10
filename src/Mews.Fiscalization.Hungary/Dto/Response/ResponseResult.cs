namespace Mews.Fiscalization.Hungary.Dto.Response
{
    public class ResponseResult<TResult>
        where TResult : class
    {
        public ResponseResult(TResult successResult = null, GeneralErrorResponse errorResult = null)
        {
            SuccessResult = successResult;
            ErrorResult = errorResult;
        }

        public TResult SuccessResult { get; }

        public GeneralErrorResponse ErrorResult { get; }
    }
}
