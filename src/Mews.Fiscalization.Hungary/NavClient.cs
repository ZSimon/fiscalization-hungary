using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mews.Fiscalization.Hungary.Dto;

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
	        var signatureData = $"{requestId}{formattedTimestamp}{TechnicalUser.XmlSigningKey}{additionalSignatureData}";
	        return Sha512.GetSha3Hash(signatureData);
        }
    }
}