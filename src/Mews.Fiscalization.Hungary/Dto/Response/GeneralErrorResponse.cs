using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto.Response
{
	[XmlRoot(ElementName = "GeneralErrorResponse", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
	public class GeneralErrorResponse
	{
		[XmlElement(ElementName = "result", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public Result Result { get; set; }

		[XmlElement(ElementName = "technicalValidationMessages", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public List<TechnicalValidationMessages> TechnicalValidationMessages { get; set; }
	}

	[XmlRoot(ElementName = "technicalValidationMessages", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
	public class TechnicalValidationMessages
	{
		[XmlElement(ElementName = "validationResultCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public string ValidationResultCode { get; set; }

		[XmlElement(ElementName = "validationErrorCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public string ValidationErrorCode { get; set; }

		[XmlElement(ElementName = "message", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public string Message { get; set; }
	}

	[XmlRoot(ElementName = "result", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
	public class Result
	{
		[XmlElement(ElementName = "funcCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public string FuncCode { get; set; }

		[XmlElement(ElementName = "errorCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public string ErrorCode { get; set; }

		[XmlElement(ElementName = "message", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
		public string Message { get; set; }
	}
}
