using Mews.Fiscalization.Hungary.Models;
using Mews.Fiscalization.Hungary.Models.TaxPayer;
using Mews.Fiscalization.Hungary.Utils;
using System.Linq;

namespace Mews.Fiscalization.Hungary
{
    internal static class ModelMapper
    {
        internal static ResponseResult<ExchangeToken> MapExchangeToken(Dto.TokenExchangeResponse response, TechnicalUser user)
        {
            var decryptedToken = Aes.Decrypt(user.EncryptionKey, response.encodedExchangeToken);
            return new ResponseResult<ExchangeToken>(successResult: new ExchangeToken(
                value: decryptedToken,
                validFrom: response.tokenValidityFrom,
                validTo: response.tokenValidityTo
            ));
        }

        internal static ResponseResult<TaxPayerData> MapTaxPayerData(Dto.QueryTaxpayerResponse response)
        {
            if (response.taxpayerValidity) // TODO - it's nullable
            {
                return new ResponseResult<TaxPayerData>(successResult: TaxPayerData.Map(response));
            }
            else
            {
                return new ResponseResult<TaxPayerData>(errorResult: new ErrorResult(ResultErrorCode.InvalidTaxPayer));
            }
        }

        internal static ResponseResult<InvoiceStatus> MapInvoiceStatus(Dto.QueryTransactionStatusResponse response)
        {
            var result = response.processingResults;
            if (result == null || result.processingResult.First() == null)
            {
                return new ResponseResult<InvoiceStatus>(errorResult: new ErrorResult(ResultErrorCode.InvalidId));
            }
            else
            {
                return new ResponseResult<InvoiceStatus>(successResult: InvoiceStatus.Map(response));
            }
        }

        internal static ResponseResult<string> MapInvoices(Dto.ManageInvoiceResponse response)
        {
            return new ResponseResult<string>(successResult: response.transactionId);
        }
    }
}