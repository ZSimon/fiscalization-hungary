using System;

namespace Mews.Fiscalization.Hungary.Model
{
    public abstract class Invoice
    {
        protected Invoice(string number, DateTime issueDate, string currencyCode, DateTime? deliveryDate = null)
        {
            Number = number;
            IssueDate = issueDate;
            DeliveryDate = deliveryDate;
            CurrencyCode = currencyCode;
        }

        public string Number { get; }

        public DateTime IssueDate { get; }

        public DateTime? DeliveryDate { get; }

        public string CurrencyCode { get; }
    }

    public sealed class AggregateInvoice : Invoice
    {
        public AggregateInvoice(string number, DateTime issueDate, string currencyCode, DateTime? deliveryDate = null) : base(number, issueDate, currencyCode, deliveryDate)
        {
        }
    }

    public class InvoiceLine
    {
        public LineModificationReferenceType lineModificationReference { get; }

        public string[] referencesToOtherLines { get; }

        public bool advanceIndicator { get; }

        public ProductCodeType[] productCodes { get; }

        public bool lineExpressionIndicator { get; }

        public LineNatureIndicatorType lineNatureIndicator { get; }

        public bool lineNatureIndicatorSpecified { get; }

        public string lineDescription { get; }

        public decimal quantity { get; }

        public bool quantitySpecified { get; }

        public UnitOfMeasureType unitOfMeasure { get; }

        public bool unitOfMeasureSpecified { get; }

        public string unitOfMeasureOwn { get; }

        public decimal unitPrice { get; }

        public bool unitPriceSpecified { get; }

        public decimal unitPriceHUF { get; }

        public bool unitPriceHUFSpecified { get; }

        public DiscountDataType lineDiscountData { get; }

        public object Item { get; }

        public bool intermediatedService { get; }

        public AggregateInvoiceLineDataType aggregateInvoiceLineData { get; }

        public NewTransportMeanType newTransportMean { get; }

        public bool depositIndicator { get; }

        public MarginSchemeType marginSchemeIndicator { get; }

        public bool marginSchemeIndicatorSpecified { get; }

        public string[] ekaerIds { get; }

        public bool obligatedForProductFee { get; }

        public decimal GPCExcise { get; }

        public bool GPCExciseSpecified { get; }

        public DieselOilPurchaseType dieselOilPurchase { get; }

        public bool netaDeclaration { get; }

        public ProductFeeClauseType productFeeClause { get; }

        public ProductFeeDataType[] lineProductFeeContent { get; }

        public AdditionalDataType[] additionalLineData { get; }
    }

    public sealed class AggregateInvoiceLine : InvoiceLine
    {

    }
}