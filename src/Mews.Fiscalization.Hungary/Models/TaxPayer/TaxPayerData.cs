using System;

namespace Mews.Fiscalization.Hungary.Models.TaxPayer
{
    public sealed class TaxPayerData
    {
        public TaxPayerData(string id, string name, Address address, string vatCode, bool isValid, DateTime? infoDate = null)
        {
            Id = id;
            Name = name;
            Address = address;
            VatCode = vatCode;
            IsValid = isValid;
            InfoDate = infoDate;
        }

        public string Id { get; }

        public string Name { get; }

        public Address Address { get; }

        public string VatCode { get; }

        public bool IsValid { get; }

        public DateTime? InfoDate { get; }

        internal static TaxPayerData Map(Dto.QueryTaxpayerResponse response)
        {
            var addressItem = response.TaxpayerData.TaxpayerAddressList.TaxpayerAddressItem;
            var taxPayerData = response.TaxpayerData;
            var taxNumberDetail = taxPayerData.TaxNumberDetail;
            return new TaxPayerData(
                id: taxNumberDetail.TaxpayerId,
                name: taxPayerData.TaxpayerName,
                address: Address.Map(addressItem),
                vatCode: taxNumberDetail.VatCode,
                isValid: response.IsValidTaxPayer,
                infoDate: response.InfoDate
            );
        }
    }
}
