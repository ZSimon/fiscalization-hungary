using System;
using System.Net;
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

        static NavClient()
        {
            HttpClient = new HttpClient();
        }

        public NavClient(TechnicalUser technicalUser, SoftwareIdentification softwareIdentification)
        {
            TechnicalUser = technicalUser;
            SoftwareIdentification = softwareIdentification;
        }

        public async Task<bool> GetTaxpayerDataAsync(string taxNumber)
        {
	        var timestamp = DateTime.UtcNow;
	        var requestId = Sha512.GetRandomRequestId();
	        var xmlSigningKey = "f8-a9d1-73227923aeb7281AVZXJHZM8";
	        var signatureData = $"{requestId}{timestamp.ToString("yyyyMMddHHmmss")}{xmlSigningKey}";
	        var requestSignature = Sha512.GetSha3Hash(signatureData);
	        var query = new QueryTaxpayerRequest
	        {
		        Header = new Header
		        {
			        RequestId = requestId,
			        TimeStamp = $"{timestamp.ToString("s")}Z"
		        },
		        User = new User
		        {
			        Login = TechnicalUser.Login,
			        PasswordHash = TechnicalUser.PasswordHash,
			        TaxNumber = TechnicalUser.TaxNumber,
			        RequestSignature = requestSignature
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
		        },
		        TaxNumber = taxNumber
	        };

	        var xml2 = XmlManipulator.Serialize(query).OuterXml;

            var xml = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
				<QueryTaxpayerRequest
					xmlns=""http://schemas.nav.gov.hu/OSA/2.0/api"">
					<header>
						<requestId>{requestId}</requestId>
						<timestamp>{timestamp.ToString("s")}Z</timestamp>
						<requestVersion>2.0</requestVersion>
						<headerVersion>1.0</headerVersion>
					</header>
					<user>
						<login>{TechnicalUser.Login}</login>
						<passwordHash>{TechnicalUser.PasswordHash}</passwordHash>
						<taxNumber>{TechnicalUser.TaxNumber}</taxNumber>
						<requestSignature>{requestSignature}</requestSignature>
					</user>
					<software>
						<softwareId>123456789123456789</softwareId>
						<softwareName>string</softwareName>
						<softwareOperation>LOCAL_SOFTWARE</softwareOperation>
						<softwareMainVersion>string</softwareMainVersion>
						<softwareDevName>string</softwareDevName>
						<softwareDevContact>string</softwareDevContact>
						<softwareDevCountryCode>HU</softwareDevCountryCode>
						<softwareDevTaxNumber>string</softwareDevTaxNumber>
					</software>
					<taxNumber>{taxNumber}</taxNumber>
				</QueryTaxpayerRequest>
			";
            var response = await HttpClient.PostAsync("https://api-test.onlineszamla.nav.gov.hu/invoiceService/v2/queryTaxpayer", new StringContent(xml, Encoding.UTF8, "application/xml"));
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}