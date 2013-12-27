using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
namespace UpsFeedToTable
{
    public class EDI_Data
    {
        [AutoIncrement]
        public int Id { get; set; }

        [StringLength(3)]
        public string Version { get; set; }

        [StringLength(10)]
        public string RecipientNumber { get; set; }

        [StringLength(10)]
        public string AccountNumber { get; set; }

        [StringLength(2)]
        public string AccountCountry { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(15)]
        public string InvoiceNumber { get; set; }

        [StringLength(1)]
        public string InvoiceTypeCode { get; set; }

        [StringLength(2)]
        public string InvoiceTypeDetailCode { get; set; }

        [StringLength(25)]
        public string AccountTaxId { get; set; }

        [StringLength(3)]
        public string InvoiceCurrencyCode { get; set; }

        public decimal InvoiceAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        [StringLength(10)]
        public string PickupRecordNumber { get; set; }

        [StringLength(26)]
        public string LeadShipmentNumber { get; set; }


        [StringLength(26)]
        public string WorldEaseNumber { get; set; }

        [StringLength(35)]
        public string ShipmentReferenceNumber1 { get; set; }

        [StringLength(35)]
        public string ShipmentReferenceNumber2 { get; set; }

        [StringLength(3)]
        public string BillOptionCode { get; set; }

        public int PackageQuantity { get; set; }

        public int OversizeQuantity { get; set; }

        [StringLength(26)]
        public string TrackingNumber { get; set; }

        [StringLength(35)]
        public string PackageReferenceNumber1 { get; set; }

        [StringLength(35)]
        public string PackageReferenceNumber2 { get; set; }       
        
        [StringLength(35)]
        public string PackageReferenceNumber3 { get; set; }

        [StringLength(35)]
        public string PackageReferenceNumber4 { get; set; }

        [StringLength(35)]
        public string PackageReferenceNumber5 { get; set; }

        public decimal EnteredWeight { get; set; }

        [StringLength(1)]
        public string EnteredWeightUnitOfMeasure { get; set; }

        public decimal BilledWeight { get; set; }

        [StringLength(1)]
        public string BilledWeightUnitOfMeasure { get; set; }

        [StringLength(3)]
        public string ContainerType { get; set; }

        [StringLength(2)]
        public string BilledWeightType { get; set; }

        [StringLength(17)]
        public string PackageDimensions { get; set; }

        [StringLength(3)]
        public string Zone { get; set; }

        [StringLength(3)]
        public string ChargeCategoryCode { get; set; }

        [StringLength(4)]
        public string ChargeCategoryDetailCode { get; set; }

        [StringLength(5)]
        public string ChargeSource { get; set; }

        [StringLength(2)]
        public string TypeCode1 { get; set; }

        [StringLength(35)]
        public string TypeDetailCode1 { get; set; }

        [StringLength(10)]
        public string TypeDetailValue1 { get; set; }

        [StringLength(2)]
        public string TypeCode2 { get; set; }

        [StringLength(35)]
        public string TypeDetailCode2 { get; set; }

        [StringLength(10)]
        public string TypeDetailValue2 { get; set; }

        [StringLength(3)]
        public string ChargeClassificationCode { get; set; }

        [StringLength(5)]
        public string ChargeDescriptionCode { get; set; }

        [StringLength(100)]
        public string ChargeDescription { get; set; }

        public int ChargedUnitQuantity { get; set; }

        [StringLength(3)]
        public string BasisCurrencyCode { get; set; }

        [StringLength(17)]
        public string BasisValue { get; set; }

        [StringLength(1)]
        public string TaxIndicator { get; set; }

        [StringLength(3)]
        public string TransactionCurrencyCode { get; set; }

        public decimal IncentiveAmount { get; set; }

        public decimal NetAmount { get; set; }

        [StringLength(3)]
        public string MiscellaneousCurrencyCode { get; set; }

        public decimal MiscellaneousIncentiveAmount { get; set; }

        public decimal MiscellaneousNetAmount { get; set; }

        [StringLength(3)]
        public string AlternateInvoicingCurrencyCode { get; set; }

        public decimal AlternateInvoiceAmount { get; set; }
        public decimal InvoiceExchangeRate { get; set; }
        public decimal TaxVarianceAmount { get; set; }
        public decimal CurrencyVarianceAmount { get; set; }
        public decimal InvoiceLevelCharge { get; set; }

        public DateTime InvoiceDueDate { get; set; }

        [StringLength(15)]
        public string AlternateInvoiceNumber { get; set; }

        [StringLength(6)]
        public string StoreNumber { get; set; }

        [StringLength(19)]
        public string CustomerReferenceNumber { get; set; }

        [StringLength(50)]
        public string SenderName { get; set; }

        [StringLength(50)]
        public string SenderCompanyName { get; set; }

        [StringLength(50)]
        public string SenderAddressLine1 { get; set; }

        [StringLength(50)]
        public string SenderAddressLine2 { get; set; }

        [StringLength(50)]
        public string SenderCity { get; set; }

        [StringLength(30)]
        public string SenderState { get; set; }

        [StringLength(12)]
        public string SenderPostal { get; set; }

        [StringLength(2)]
        public string SenderCountry { get; set; }

        [StringLength(50)]
        public string ReceiverName { get; set; }

        [StringLength(50)]
        public string ReceiverCompanyName { get; set; }

        [StringLength(50)]
        public string ReceiverAddressLine1 { get; set; }

        [StringLength(50)]
        public string ReceiverAddressLine2 { get; set; }

        [StringLength(50)]
        public string ReceiverCity { get; set; }

        [StringLength(30)]
        public string ReceiverState { get; set; }

        [StringLength(12)]
        public string ReceiverPostal { get; set; }

        [StringLength(2)]
        public string ReceiverCountry { get; set; }

        [StringLength(50)]
        public string ThirdPartyName { get; set; }

        [StringLength(50)]
        public string ThirdPartyCompanyName { get; set; }

        [StringLength(50)]
        public string ThirdPartyAddressLine1 { get; set; }

        [StringLength(50)]
        public string ThirdPartyAddressLine2 { get; set; }

        [StringLength(50)]
        public string ThirdPartyCity { get; set; }

        [StringLength(30)]
        public string ThirdPartyState { get; set; }

        [StringLength(12)]
        public string ThirdPartyPostal { get; set; }

        [StringLength(2)]
        public string ThirdPartyCountry { get; set; }

        [StringLength(50)]
        public string SoldToName { get; set; }

        [StringLength(50)]
        public string SoldToCompanyName { get; set; }

        [StringLength(50)]
        public string SoldToAddressLine1 { get; set; }

        [StringLength(50)]
        public string SoldToAddressLine2 { get; set; }

        [StringLength(50)]
        public string SoldToCity { get; set; }

        [StringLength(30)]
        public string SoldToState { get; set; }

        [StringLength(12)]
        public string SoldToPostal { get; set; }

        [StringLength(2)]
        public string SoldToCountry { get; set; }

        [StringLength(1)]
        public string MiscellaneousAddressQual1 { get; set; }
        [StringLength(50)]

        public string MiscellaneousAddress1Name { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress1CompanyName { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress1AddressLine1 { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress1AddressLine2 { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress1City { get; set; }

        [StringLength(30)]
        public string MiscellaneousAddress1State { get; set; }

        [StringLength(12)]
        public string MiscellaneousAddress1Postal { get; set; }

        [StringLength(2)]
        public string MiscellaneousAddress1Country { get; set; }

        [StringLength(1)]
        public string MiscellaneousAddressQual2 { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress2Name { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress2CompanyName { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress2AddressLine1 { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress2AddressLine2 { get; set; }

        [StringLength(50)]
        public string MiscellaneousAddress2City { get; set; }

        [StringLength(30)]
        public string MiscellaneousAddress2State { get; set; }

        [StringLength(12)]
        public string MiscellaneousAddress2Postal { get; set; }

        [StringLength(2)]
        public string MiscellaneousAddress2Country { get; set; }

        public DateTime ShipmentDate { get; set; }

        public DateTime ShipmentExportDate { get; set; }

        public DateTime ShipmentImportDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DirectShipmentDate { get; set; }

        //weird, this is string according to UPS xls
        [StringLength(50)]
        public string ShipmentDeliveryDate { get; set; }

        public DateTime ShipmentReleaseDate { get; set; }

        [StringLength(50)]
        public string CycleDate { get; set; }

        [StringLength(50)]
        public string EFTDate { get; set; }

        [StringLength(50)]
        public string ValidationDate { get; set; }

        [StringLength(4)]
        public string EntryPort { get; set; }

        [StringLength(50)]
        public string EntryNumber { get; set; }

        [StringLength(2)]
        public string ExportPlace { get; set; }

        public decimal ShipmentValueAmount { get; set; }

        [StringLength(50)]
        public string ShipmentDescription { get; set; }

        [StringLength(3)]
        public string EnteredCurrencyCode { get; set; }

        [StringLength(20)]
        public string CustomsNumber { get; set; }

        public decimal ExchangeRate { get; set; }

        [StringLength(50)]
        public string MasterAirWayBillNumber { get; set; }

        [StringLength(50)]
        public string EPU { get; set; }

        [StringLength(50)]
        public string EntryType { get; set; }

        [StringLength(50)] 
        public string CPCCode { get; set; }

        public int LineItemNumber { get; set; }

        [StringLength(35)]
        public string GoodsDescription { get; set; }

        public decimal EnteredValue { get; set; }

        public decimal DutyAmount { get; set; }
        
        [StringLength(10)]
        public string Weight { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure { get; set; }

        public int ItemQuantity { get; set; }

        [StringLength(14)]
        public string ItemQuantityUnitOfMeasure { get; set; }

        [StringLength(14)]
        public string ImportTaxId { get; set; }

        [StringLength(50)]
        public string DeclarationNumber { get; set; }

        [StringLength(50)]
        public string CarrierName { get; set; }

        [StringLength(15)]
        public string CCCDNumber { get; set; }

        [StringLength(50)]
        public string CycleNumber { get; set; }

        [StringLength(50)]
        public string ForeignTradeReferenceNumber { get; set; }

        [StringLength(50)]
        public string JobNumber { get; set; }

        [StringLength(4)]
        public string TransportMode { get; set; }

        [StringLength(2)]
        public string TaxType { get; set; }

        [StringLength(50)]
        public string TariffCode { get; set; }

        public decimal TariffRate { get; set; }

        [StringLength(11)]
        public string TariffTreatmentNumber { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(15)]
        public string ClassNumber { get; set; }

        [StringLength(50)]
        public string DocumentType { get; set; }

        [StringLength(50)]
        public string OfficeNumber { get; set; }

        [StringLength(50)]
        public string DocumentNumber { get; set; }

        public decimal DutyValue { get; set; }
        public decimal TotalValueForDuty { get; set; }

        public decimal ExciseTaxAmount { get; set; }

        public decimal ExciseTaxRate { get; set; }
        public decimal GSTAmount { get; set; }

        public decimal GSTRate { get; set; }

        [StringLength(16)]
        public string OrderInCouncil { get; set; }

        [StringLength(2)]
        public string OriginCountry { get; set; }

        public decimal SIMAAccess { get; set; }
        public decimal TaxValue{ get; set; }
        public decimal TotalCustomsAmount { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine1 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine2 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine3 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine4 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine5 { get; set; }

        [StringLength(2)]
        public string PayerRoleCode { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine7 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine8 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine9 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine10 { get; set; }

        [StringLength(50)]
        public string MiscellaneousLine11 { get; set; }

        public decimal DutyRate { get; set; }

        public decimal VATBasisAmount { get; set; }

        public decimal VATAmount { get; set; }

        public decimal VATRate { get; set; }

        public decimal OtherBasisAmount { get; set; }

        public decimal OtherAmount { get; set; }

        public decimal OtherRate { get; set; }

        [StringLength(1)]
        public string OtherCustomsNumberIndicator { get; set; }

        [StringLength(35)]
        public string OtherCustomsNumber { get; set; }

        [StringLength(15)]
        public string CustomsOfficeName { get; set; }

        [StringLength(1)]
        public string PackageDimensionUnitOfMeasure { get; set; }

        public int OriginalShipmentPackageQuantity { get; set; }

        [StringLength(3)]
        public string CorrectedZone { get; set; }

        [StringLength(5)]
        public string TaxLawArticleNumber { get; set; }

        public decimal TaxLawArticleBasisAmount { get; set; }

        [StringLength(26)]
        public string OriginalTrackingNumber { get; set; }

        public decimal ScaleWeightQuantity { get; set; }

        [StringLength(1)]
        public string ScaleWeightUnitOfMeasure { get; set; }

        [StringLength(1)]
        public string RawDimensionsUnitOfMeasure { get; set; }

        [StringLength(23)]
        public string RawDimensions { get; set; }

        [StringLength(20)]
        public string BOL1 { get; set; }

        [StringLength(20)]
        public string BOL2 { get; set; }

        [StringLength(20)]
        public string BOL3 { get; set; }

        [StringLength(20)]
        public string BOL4 { get; set; }

        [StringLength(20)]
        public string BOL5 { get; set; }

        [StringLength(20)]
        public string PO1 { get; set; }

        [StringLength(20)]
        public string PO2 { get; set; }

        [StringLength(20)]
        public string PO3 { get; set; }

        [StringLength(20)]
        public string PO4 { get; set; }

        [StringLength(20)]
        public string PO5 { get; set; }

        [StringLength(20)]
        public string PO6 { get; set; }

        [StringLength(20)]
        public string PO7 { get; set; }

        [StringLength(20)]
        public string PO8 { get; set; }

        [StringLength(20)]
        public string PO9 { get; set; }

        [StringLength(20)]
        public string PO10 { get; set; }

        [StringLength(8)]
        public string NMFC { get; set; }

        [StringLength(4)]
        public string DetailClass { get; set; }

        public int FreightSequenceNumber { get; set; }

        [StringLength(4)]
        public string DeclaredFreightClass { get; set; }

        [StringLength(35)]
        public string EORINumber { get; set; }

        [StringLength(17)]
        public string DetailKeyedDim { get; set; }

        [StringLength(1)]
        public string DetailKeyedUnitOfMeasure { get; set; }

        [StringLength(17)]
        public string DetailKeyedBilledDimensions { get; set; }

        [StringLength(1)]
        public string DetailKeyedBilledUnitOfMeasure { get; set; }

        [StringLength(100)]
        public string OriginalServiceDescription { get; set; }
    }
}
