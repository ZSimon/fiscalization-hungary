using Mews.Fiscalization.Hungary.Models;
using Mews.Fiscalization.Hungary.Models.TaxPayer;
using Mews.Fiscalization.Hungary.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Hungary
{
    public sealed class NavClient
    {
        private TechnicalUser TechnicalUser { get; }

        private SoftwareIdentification SoftwareIdentification { get; }

        private static HttpClient HttpClient { get; }

        private Client Client { get; }

        static NavClient()
        {
            HttpClient = new HttpClient();
        }

        public NavClient(TechnicalUser technicalUser, SoftwareIdentification softwareIdentification, NavEnvironment environment)
        {
            TechnicalUser = technicalUser;
            SoftwareIdentification = softwareIdentification;
            Client = new Client(HttpClient, environment);
        }

        public async Task<ResponseResult<ExchangeToken, ExchangeTokenErrorCode>> GetExchangeTokenAsync()
        {
            var request = RequestCreator.CreateTokenExchangeRequest(TechnicalUser, SoftwareIdentification);
            return await Client.ProcessRequestAsync<Dto.TokenExchangeRequest, Dto.TokenExchangeResponse, ExchangeToken, ExchangeTokenErrorCode>(
                endpoint: "tokenExchange",
                request: request,
                successFunc: response => ModelMapper.MapExchangeToken(response, TechnicalUser)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<TransactionStatus, TransactionErrorCode>> GetTransactionStatusAsync(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentException($"{nameof(transactionId)} must be specified.");
            }

            var request = RequestCreator.CreateQueryTransactionStatusRequest(TechnicalUser, SoftwareIdentification, transactionId);
            return await Client.ProcessRequestAsync<Dto.QueryTransactionStatusRequest, Dto.QueryTransactionStatusResponse, TransactionStatus, TransactionErrorCode>(
                endpoint: "queryTransactionStatus",
                request: request,
                successFunc: response => ModelMapper.MapTransactionStatus(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<TaxPayerData, TaxPayerErrorCode>> GetTaxPayerDataAsync(TaxPayerId taxId)
        {
            var request = RequestCreator.CreateQueryTaxpayerRequest(TechnicalUser, SoftwareIdentification, taxId.Value);
            return await Client.ProcessRequestAsync<Dto.QueryTaxpayerRequest, Dto.QueryTaxpayerResponse, TaxPayerData, TaxPayerErrorCode>(
                endpoint: "queryTaxpayer",
                request: request,
                successFunc: response => ModelMapper.MapTaxPayerData(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<string, ResultErrorCode>> SendInvoicesAsync(ExchangeToken token, IEnumerable<IndexedItem<Invoice>> invoices)
        {
            var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
            return await Client.ProcessRequestAsync<Dto.ManageInvoiceRequest, Dto.ManageInvoiceResponse, string, ResultErrorCode>(
                endpoint: "manageInvoice",
                request: request,
                successFunc: response => ModelMapper.MapInvoices(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}