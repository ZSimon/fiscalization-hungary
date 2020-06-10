using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mews.Fiscalization.Hungary.Dto;
using Mews.Fiscalization.Hungary.Dto.Response;
using Mews.Fiscalization.Hungary.Model;

namespace Mews.Fiscalization.Hungary
{
	public sealed class NavClient
    {
        private TechnicalUser TechnicalUser { get; }

        private SoftwareIdentification SoftwareIdentification { get; }

        private static HttpClient HttpClient { get; }

        private NavEnvironment Environment { get; }

        static NavClient()
        {
            HttpClient = new HttpClient();
        }

        public NavClient(TechnicalUser technicalUser, SoftwareIdentification softwareIdentification, NavEnvironment environment)
        {
            TechnicalUser = technicalUser;
            SoftwareIdentification = softwareIdentification;
            Environment = environment;
        }

        public async Task<ExchangeToken> GetExchangeTokenAsync()
        {
	        var request = CreateRequest<TokenExchangeRequest>();
	        var httpResponse = await SendRequestAsync("tokenExchange", request);
	        var responseXml = await httpResponse.Content.ReadAsStringAsync();
	        var response = XmlManipulator.Deserialize<TokenExchangeResponse>(responseXml);
	        var tokenBase64 = response.EncodedToken;
	        var tokenData = Convert.FromBase64String(tokenBase64);
	        var decryptedToken = Aes.Decrypt(TechnicalUser.EncryptionKey, tokenData);
	        return new ExchangeToken(decryptedToken, response.ValidFrom, response.ValidTo);
        }

        public async Task SendInvoicesAsync(ExchangeToken exchangeToken, IEnumerable<Invoice> invoices)
        {
	        var request = CreateRequest<ManageInvoiceRequest>();
	        request.ExchangeToken = exchangeToken.Value;
	        request.Operations = new InvoiceOperations
	        {
		        CompressedContent = false,
		        Items = invoices.Select(i => CreateInvoiceOperation(i)).ToList()
	        };
	        await SendRequestAsync("manageInvoice", request);
        }

        private InvoiceOperation CreateInvoiceOperation(Invoice invoice)
        {
	        throw new NotImplementedException();
        }

        private async Task<HttpResponseMessage> SendRequestAsync<TRequest>(string endpoint, TRequest request)
			where TRequest : class
        {
	        var content = new StringContent(XmlManipulator.Serialize(request), Encoding.UTF8, "application/xml");
	        var url = new Uri(new Uri(ServiceInfo.BaseUrls[Environment], ServiceInfo.RelativeServiceUrl), endpoint);
	        return await HttpClient.PostAsync(url, content);
        }

        private TRequest CreateRequest<TRequest>(string additionalSignatureData = null)
			where TRequest : Request, new()
        {
	        var requestId = RequestId.CreateRandom();
	        var timestamp = DateTime.UtcNow;
			return new TRequest
	        {
		        Header = new Header
		        {
			        RequestId = requestId,
			        TimeStamp = timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ")
		        },
		        User = new User
		        {
			        Login = TechnicalUser.Login,
			        PasswordHash = TechnicalUser.PasswordHash,
			        TaxNumber = TechnicalUser.TaxNumber,
			        RequestSignature = GetRequestSignature(requestId, timestamp, additionalSignatureData)
		        },
		        Software = new Software
		        {
			        Id = SoftwareIdentification.Id,
			        Name = SoftwareIdentification.Name,
			        Operation = SoftwareIdentification.Operation,
			        MainVersion = SoftwareIdentification.MainVersion,
			        DeveloperName = SoftwareIdentification.DeveloperName,
			        DeveloperContact = SoftwareIdentification.DeveloperContact,
			        DeveloperCountry = SoftwareIdentification.DeveloperCountry,
			        DeveloperTaxNumber = SoftwareIdentification.DeveloperTaxNumber
		        }
	        };
        }

        private string GetRequestSignature(string requestId, DateTime timestamp, string additionalSignatureData = null)
        {
	        var formattedTimestamp = timestamp.ToString("yyyyMMddHHmmss");
	        var signatureData = $"{requestId}{formattedTimestamp}{TechnicalUser.SigningKey}{additionalSignatureData}";
	        return Sha512.GetSha3Hash(signatureData);
        }
    }
}