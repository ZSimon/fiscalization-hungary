using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ErrorResult
    {
        internal ErrorResult(string message, ResultErrorCode errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public string Message { get; }

        public ResultErrorCode ErrorCode { get; }

        internal static ErrorResult Map(Dto.GeneralErrorResponse response)
        {
            return new ErrorResult(
                message: response.Result.Message,
                errorCode: MapErrorCode(response.Result.ErrorCode)
            );
        }

        internal static ResultErrorCode MapErrorCode(Dto.ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case Dto.ErrorCode.INVALID_SECURITY_USER:
                case Dto.ErrorCode.NOT_REGISTERED_CUSTOMER:
                case Dto.ErrorCode.INVALID_CUSTOMER:
                    return ResultErrorCode.InvalidCredentials;
                case Dto.ErrorCode.MAINTENANCE_MODE:
                    return ResultErrorCode.MaintenanceMode;
                default:
                    throw new NotImplementedException($"Error code: {errorCode} is not implemented.");
            }
        }
    }
}
