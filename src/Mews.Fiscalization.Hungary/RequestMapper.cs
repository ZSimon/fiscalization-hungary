using Mews.Fiscalization.Hungary.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary
{
    internal static class RequestMapper
    {
        internal static Dto.InvoiceData MapInvoice(Invoice invoice)
        {
            var supplierInfo = invoice.SupplierInfo;
            var customerInfo = invoice.CustomerInfo;
            return new Dto.InvoiceData
            {
                invoiceIssueDate = invoice.IssueDate,
                invoiceNumber = invoice.Number,
                invoiceMain = new Dto.InvoiceMainType
                {
                    Items = new Dto.InvoiceType[]
                    {
                        new Dto.InvoiceType
                        {
                            invoiceLines = MapItems(invoice.Items).ToArray(),
                            invoiceHead = new Dto.InvoiceHeadType
                            {
                                invoiceDetail = new Dto.InvoiceDetailType
                                {
                                    currencyCode = invoice.CurrencyCode,
                                    invoiceAppearance = Dto.InvoiceAppearanceType.ELECTRONIC,
                                    invoiceCategory = Dto.InvoiceCategoryType.AGGREGATE,
                                    invoiceDeliveryDate = invoice.DeliveryDate,
                                    paymentDate = invoice.PaymentDate,
                                    selfBillingIndicator = invoice.IsSelfBilling,
                                    cashAccountingIndicator = invoice.IsCashAccounting
                                },
                                supplierInfo = new Dto.SupplierInfoType
                                {
                                    supplierName = supplierInfo.Name,
                                    supplierAddress = MapAddress(supplierInfo.Address),
                                    supplierTaxNumber = MapTaxNumber(supplierInfo)
                                },
                                customerInfo = new Dto.CustomerInfoType
                                {
                                    customerName = customerInfo.Name,
                                    customerAddress = MapAddress(customerInfo.Address),
                                    customerTaxNumber = MapTaxNumber(customerInfo)
                                },
                            },
                            invoiceSummary = new Dto.SummaryType
                            {
                                summaryGrossData = new Dto.SummaryGrossDataType
                                {
                                    invoiceGrossAmount = invoice.Amount.Gross,
                                    invoiceGrossAmountHUF = invoice.AmountHUF.Gross
                                },
                                Items = new Dto.SummaryNormalType[]
                                {
                                    MapTaxSummary(invoice)
                                }
                            }
                        }
                    }
                }
            };
        }

        internal static Dto.SummaryNormalType MapTaxSummary(Invoice invoice)
        {
            return new Dto.SummaryNormalType
            {
                invoiceNetAmount = invoice.Amount.Net,
                invoiceNetAmountHUF = invoice.AmountHUF.Net,
                invoiceVatAmount = invoice.Amount.Tax,
                invoiceVatAmountHUF = invoice.AmountHUF.Tax,
                summaryByVatRate = new Dto.SummaryByVatRateType[]
                {
                    new Dto.SummaryByVatRateType
                    {
                        vatRate = new Dto.VatRateType
                        {
                            Item = invoice.TaxSummary.Percentage,
                            ItemElementName = Dto.ItemChoiceType1.vatPercentage
                        },
                        vatRateNetData = new Dto.VatRateNetDataType
                        {
                            vatRateNetAmount = invoice.TaxSummary.Amount.Net,
                            vatRateNetAmountHUF = invoice.TaxSummary.AmountHUF.Net
                        },
                        vatRateVatData = new Dto.VatRateVatDataType
                        {
                            vatRateVatAmount = invoice.TaxSummary.Amount.Tax,
                            vatRateVatAmountHUF = invoice.TaxSummary.AmountHUF.Tax
                        }
                    }
                }
            };
        }

        internal static Dto.TaxNumberType MapTaxNumber(Info info)
        {
            return new Dto.TaxNumberType
            {
                taxpayerId = info.TaxpayerId,
                vatCode = info.VatCode
            };
        }

        internal static Dto.AddressType MapAddress(SimpleAddress address)
        {
            return new Dto.AddressType
            {
                Item = new Dto.SimpleAddressType
                {
                    additionalAddressDetail = address.AddtionalAddressDetail,
                    city = address.City,
                    countryCode = address.CountryCode,
                    postalCode = address.PostalCode,
                    region = address.Region
                }
            };
        }


        internal static Dto.LineAmountsNormalType MapLineAmounts(Item item)
        {
            return new Dto.LineAmountsNormalType
            {
                lineGrossAmountData = new Dto.LineGrossAmountDataType
                {
                    lineGrossAmountNormal = item.TaxSummary.Amount.Gross,
                    lineGrossAmountNormalHUF = item.TaxSummary.AmountHUF.Gross
                },
                lineNetAmountData = new Dto.LineNetAmountDataType
                {
                    lineNetAmount = item.TaxSummary.Amount.Net,
                    lineNetAmountHUF = item.TaxSummary.AmountHUF.Net
                },
                lineVatRate = new Dto.VatRateType
                {
                    Item = item.TaxSummary.Percentage,
                    ItemElementName = Dto.ItemChoiceType1.vatPercentage
                }
            };
        }

        internal static IEnumerable<Dto.LineType> MapItems(IEnumerable<Item> items)
        {
            return items.Select(i => new Dto.LineType
            {
                lineNumber = i.Number,
                lineDescription = i.Description,
                quantity = i.Quantity,
                unitOfMeasureOwn = i.MeasurementUnit.ToString(),
                unitPrice = i.UnitAmount,
                quantitySpecified = true,
                unitOfMeasureSpecified = true,
                unitPriceSpecified = true,
                depositIndicator = i.IsDeposit,
                Item = MapLineAmounts(i),
                aggregateInvoiceLineData = new Dto.AggregateInvoiceLineDataType
                {
                    lineDeliveryDate = i.ConsumptionDate
                },
                productCodes = new Dto.ProductCodeType[]
                {
                    new Dto.ProductCodeType
                    {
                        productCodeCategory = (Dto.ProductCodeCategoryType)i.ProductCodeCategory,
                        ItemElementName = (Dto.ItemChoiceType)i.ProductCodeChoiceType,
                        Item = i.ProductCode
                    }
                }
            });
        }
    }
}
