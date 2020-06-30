using System;
using Mews.Fiscalization.Hungary.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary
{
    internal static class RequestMapper
    {
        internal static Dto.InvoiceData MapModificationDocument(ModificationDocument modificationDocument)
        {
            throw new NotImplementedException();
        }

        internal static Dto.InvoiceData MapInvoice(Invoice invoice)
        {
            var invoiceAmount = Amount.Sum(invoice.Items.Select(i => i.Item.TotalAmounts.Amount));
            var invoiceAmountHUF = Amount.Sum(invoice.Items.Select(i => i.Item.TotalAmounts.AmountHUF));
            var supplierInfo = invoice.SupplierInfo;
            var customerInfo = invoice.CustomerInfo;
            return new Dto.InvoiceData
            {
                invoiceIssueDate = invoice.IssueDate,
                invoiceNumber = invoice.Number.Value,
                invoiceMain = new Dto.InvoiceMainType
                {
                    Items = new object[]
                    {
                        new Dto.InvoiceType
                        {
                            invoiceLines = MapItems(invoice.Items).ToArray(),
                            invoiceHead = new Dto.InvoiceHeadType
                            {
                                invoiceDetail = new Dto.InvoiceDetailType
                                {
                                    exchangeRate = invoice.ExchangeRate.Value,
                                    currencyCode = invoice.CurrencyCode.Value,
                                    invoiceAppearance = Dto.InvoiceAppearanceType.ELECTRONIC,
                                    invoiceCategory = Dto.InvoiceCategoryType.AGGREGATE,
                                    invoiceDeliveryDate = invoice.DeliveryDate,
                                    paymentDate = invoice.PaymentDate,
                                    selfBillingIndicator = invoice.IsSelfBilling,
                                    cashAccountingIndicator = invoice.IsCashAccounting
                                },
                                supplierInfo = new Dto.SupplierInfoType
                                {
                                    supplierName = supplierInfo.Name.Value,
                                    supplierAddress = MapAddress(supplierInfo.Address),
                                    supplierTaxNumber = new Dto.TaxNumberType
                                    {
                                        taxpayerId = supplierInfo.TaxpayerId.Value,
                                        vatCode = supplierInfo.VatCode.Value
                                    }
                                },
                                customerInfo = new Dto.CustomerInfoType
                                {
                                    customerName = customerInfo.Name.Value,
                                    customerAddress = MapAddress(customerInfo.Address),
                                    customerTaxNumber = new Dto.TaxNumberType
                                    {
                                        taxpayerId = customerInfo.TaxpayerId.Value
                                    }
                                },
                            },
                            invoiceSummary = new Dto.SummaryType
                            {
                                summaryGrossData = new Dto.SummaryGrossDataType
                                {
                                    invoiceGrossAmount = invoiceAmount.Gross.Value,
                                    invoiceGrossAmountHUF = invoiceAmountHUF.Gross.Value
                                },
                                Items = new object[]
                                {
                                    MapTaxSummary(invoice, invoiceAmount, invoiceAmountHUF)
                                }
                            }
                        }
                    }
                }
            };
        }

        private static Dto.SummaryNormalType MapTaxSummary(Invoice invoice, Amount amount, Amount amountHUF)
        {
            return new Dto.SummaryNormalType
            {
                invoiceNetAmount = amount.Net.Value,
                invoiceNetAmountHUF = amountHUF.Net.Value,
                invoiceVatAmount = amount.Tax.Value,
                invoiceVatAmountHUF = amountHUF.Tax.Value,
                summaryByVatRate = invoice.TaxSummary.Select(s => MapSummaryByVatRate(s)).ToArray()
            };
        }

        private static Dto.SummaryByVatRateType MapSummaryByVatRate(TaxSummaryItem taxSummary)
        {
            return new Dto.SummaryByVatRateType
            {
                vatRate = GetVatRate(taxSummary.TaxRatePercentage),
                vatRateNetData = new Dto.VatRateNetDataType
                {
                    vatRateNetAmount = taxSummary.Amount.Net.Value,
                    vatRateNetAmountHUF = taxSummary.AmountHUF.Net.Value
                },
                vatRateVatData = new Dto.VatRateVatDataType
                {
                    vatRateVatAmount = taxSummary.Amount.Tax.Value,
                    vatRateVatAmountHUF = taxSummary.AmountHUF.Tax.Value
                }
            };
        }

        private static Dto.AddressType MapAddress(SimpleAddress address)
        {
            return new Dto.AddressType
            {
                Item = new Dto.SimpleAddressType
                {
                    additionalAddressDetail = address.AddtionalAddressDetail.Value,
                    city = address.City.Value,
                    countryCode = address.CountryCode.Value,
                    postalCode = address.PostalCode.Value,
                    region = address.Region?.Value
                }
            };
        }


        private static Dto.LineAmountsNormalType MapLineAmounts(InvoiceItem item)
        {
            return new Dto.LineAmountsNormalType
            {
                lineGrossAmountData = new Dto.LineGrossAmountDataType
                {
                    lineGrossAmountNormal = item.TotalAmounts.Amount.Gross.Value,
                    lineGrossAmountNormalHUF = item.TotalAmounts.AmountHUF.Gross.Value
                },
                lineNetAmountData = new Dto.LineNetAmountDataType
                {
                    lineNetAmount = item.TotalAmounts.Amount.Net.Value,
                    lineNetAmountHUF = item.TotalAmounts.AmountHUF.Net.Value
                },
                lineVatRate = GetVatRate(item.TotalAmounts.TaxRatePercentage)
            };
        }

        private static IEnumerable<Dto.LineType> MapItems(ISequentialEnumerable<InvoiceItem> items)
        {
            return items.Select(i => new Dto.LineType
            {
                lineNumber = i.Index.ToString(),
                lineDescription = i.Item.Description.Value,
                quantity = i.Item.Quantity,
                unitOfMeasureOwn = i.Item.MeasurementUnit.ToString(),
                unitPrice = i.Item.UnitAmounts.Amount.Net.Value,
                unitPriceHUF = i.Item.UnitAmounts.AmountHUF.Net.Value,
                quantitySpecified = true,
                unitOfMeasureSpecified = true,
                unitPriceSpecified = true,
                unitPriceHUFSpecified = true,
                depositIndicator = i.Item.IsDeposit,
                Item = MapLineAmounts(i.Item),
                aggregateInvoiceLineData = new Dto.AggregateInvoiceLineDataType
                {
                    lineExchangeRateSpecified = true,
                    lineExchangeRate = i.Item.ExchangeRate?.Value ?? 0m,
                    lineDeliveryDate = i.Item.ConsumptionDate
                }
            });
        }

        private static Dto.VatRateType GetVatRate(decimal? taxRatePercentage)
        {
            if (taxRatePercentage.HasValue)
            {
                return new Dto.VatRateType
                {
                    Item = taxRatePercentage,
                    ItemElementName = Dto.ItemChoiceType1.vatPercentage
                };
            }

            return new Dto.VatRateType
            {
                Item = true,
                ItemElementName = Dto.ItemChoiceType1.vatOutOfScope
            };
        }
    }
}
