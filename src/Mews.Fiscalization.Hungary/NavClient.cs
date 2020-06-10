using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mews.Fiscalization.Hungary.Dto;
using Mews.Fiscalization.Hungary.Dto.QueryTaxpayer;
using Mews.Fiscalization.Hungary.Dto.Response;

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

		public async Task<ResponseResult<QueryTaxpayerResponse>> GetTaxPayerDataAsync(string taxNumber)
		{
			var request = CreateRequest<QueryTaxpayerRequest>();
			request.TaxNumber = taxNumber;

			return await SendRequestAsync<QueryTaxpayerRequest, QueryTaxpayerResponse>("queryTaxpayer", request);
		}

		private async Task<ResponseResult<TResult>> SendRequestAsync<TRequest, TResult>(string endpoint, TRequest request)
			where TRequest : class
			where TResult : class
        {
	        var content = new StringContent(XmlManipulator.Serialize(request), Encoding.UTF8, "application/xml");
			var uri = new Uri(ServiceInfo.BaseUrls[Environment], $"{ServiceInfo.RelativeServiceUrl}{endpoint}");
	        var response = await HttpClient.PostAsync(uri, content);

			if (response.IsSuccessStatusCode)
            {
				var successResult = XmlManipulator.Deserialize<TResult>(await response.Content.ReadAsStringAsync());
				return new ResponseResult<TResult>(successResult: successResult);
			}
			else
            {
				var errorResult = XmlManipulator.Deserialize<GeneralErrorResponse>(await response.Content.ReadAsStringAsync());
				return new ResponseResult<TResult>(errorResult: errorResult);
			}
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
			var signatureData = $"{requestId}{formattedTimestamp}{TechnicalUser.XmlSigningKey}{additionalSignatureData}";
			return Sha512.GetSha3Hash(signatureData);
        }
    }
}