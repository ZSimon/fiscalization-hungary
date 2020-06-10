using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Dto
{
    [XmlRoot(ElementName = "QueryTaxpayerResponse", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class QueryTaxpayerResponse : BasicResponse
    {
        [XmlElement(ElementName = "infoDate", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public DateTime? InfoDate { get; set; }

        [XmlElement(ElementName = "taxpayerData", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public TaxpayerData TaxpayerData { get; set; }
    }

    [XmlRoot(ElementName = "taxpayerData", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class TaxpayerData
    {
        [XmlElement(ElementName = "taxpayerName", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public string TaxpayerName { get; set; }

        [XmlElement(ElementName = "taxNumberDetail", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public TaxNumberDetail TaxNumberDetail { get; set; }

        [XmlElement(ElementName = "taxpayerAddressList", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public TaxpayerAddressList TaxpayerAddressList { get; set; }
    }

    [XmlRoot(ElementName = "taxNumberDetail", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class TaxNumberDetail
    {
        [XmlElement(ElementName = "taxpayerId", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string TaxpayerId { get; set; }

        [XmlElement(ElementName = "vatCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string VatCode { get; set; }
    }

    [XmlRoot(ElementName = "taxpayerAddressItem", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class TaxpayerAddressItem
    {
        [XmlElement(ElementName = "taxpayerAddressType", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public string TaxpayerAddressType { get; set; }

        [XmlElement(ElementName = "taxpayerAddress", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public TaxpayerAddress TaxpayerAddress { get; set; }
    }

    [XmlRoot(ElementName = "taxpayerAddressList", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class TaxpayerAddressList
    {
        [XmlElement(ElementName = "taxpayerAddressItem", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
        public TaxpayerAddressItem TaxpayerAddressItem { get; set; }
    }

    [XmlRoot(ElementName = "taxpayerAddress", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/api")]
    public class TaxpayerAddress
    {
        [XmlElement(ElementName = "countryCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string CountryCode { get; set; }

        [XmlElement(ElementName = "postalCode", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "city", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string City { get; set; }

        [XmlElement(ElementName = "streetName", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string StreetName { get; set; }

        [XmlElement(ElementName = "number", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string Number { get; set; }

        [XmlElement(ElementName = "floor", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string Floor { get; set; }

        [XmlElement(ElementName = "door", Namespace = "http://schemas.nav.gov.hu/OSA/2.0/data")]
        public string Door { get; set; }
    }
}