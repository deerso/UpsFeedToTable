using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;

namespace UpsFeedToTable
{
    class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.Run();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }

    public class App
    {
        public void Run()
        {

            var testFileName = @"C:\Users\kgobe_000\SkyDrive\UPSFeeds\Invoice_0000007V013V013_010513.csv";
      
            var connectionString = ConfigUtils.GetConnectionString("databaseConnection");

            IDbConnectionFactory connectionFactory = new OrmLiteConnectionFactory(connectionString,new SqlServerOrmLiteDialectProvider());
            using (var db = connectionFactory.Open())
            {
                db.CreateTableIfNotExists<EDI_Data>();

                db.InsertAll(KyleFileReader.GetDataForFile(testFileName));
            }
        }

      
    }

    public class KyleFileReader
    {
        public KyleFileReader()
        {
            
        }
        public static decimal ParseDecimal(string s)
        {
            var value = decimal.Zero;

            decimal.TryParse(s, out value);

            return value;
        }

        public static int ParseInt(string s)
        {
            var value = 0;
            int.TryParse(s, out value);
            return value;
        }

        public static DateTime? ParseDateTime(string s)
        {
            if (s.IsNullOrEmpty())
                return null;
            return DateTime.Parse(s);
        }
        public static IEnumerable<EDI_Data> GetDataForFile(string filename)
        {
            var lines = File.ReadLines(filename);

            var data = lines.Select(x => Regex.Split(x, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"));

            return data.Select(x => new EDI_Data
            {
                Version = x[0],
                RecipientNumber = x[1],
                AccountNumber = x[2],
                AccountCountry = x[3],
                InvoiceDate = ParseDateTime(x[4]),
                InvoiceNumber = x[5],
                InvoiceTypeCode = x[6],
                InvoiceTypeDetailCode = x[7],
                AccountTaxId = x[8],
                InvoiceCurrencyCode = x[9],
                InvoiceAmount = ParseDecimal(x[10]),
                TransactionDate = ParseDateTime(x[11]),
                PickupRecordNumber = x[12],
                LeadShipmentNumber = x[13],
                WorldEaseNumber = x[14],
                ShipmentReferenceNumber1 = x[15],
                ShipmentReferenceNumber2 = x[16],
                BillOptionCode = x[17],
                PackageQuantity = ParseInt(x[18]),
                OversizeQuantity = ParseInt(x[19]),
                TrackingNumber = x[20],
                PackageReferenceNumber1 = x[21],
                PackageReferenceNumber2 = x[22],
                PackageReferenceNumber3 = x[23],
                PackageReferenceNumber4 = x[24],
                PackageReferenceNumber5 = x[25],
                EnteredWeight = ParseDecimal(x[26]),
                EnteredWeightUnitOfMeasure = x[27],
                BilledWeight = ParseDecimal(x[28]),
                BilledWeightUnitOfMeasure = x[29],
                ContainerType = x[30],
                BilledWeightType = x[31],
                PackageDimensions = x[32],
                Zone = x[33],
                ChargeCategoryCode = x[34],
                ChargeCategoryDetailCode = x[35],
                ChargeSource = x[36],
                TypeCode1 = x[37],
                TypeDetailCode1 = x[38],
                TypeDetailValue1 = x[39],
                TypeCode2 = x[40],
                TypeDetailCode2 = x[41],
                TypeDetailValue2 = x[42],
                ChargeClassificationCode = x[43],
                ChargeDescriptionCode = x[44],
                ChargeDescription = x[45],
                ChargedUnitQuantity = ParseInt(x[46]),
                BasisCurrencyCode = x[47],
                BasisValue = x[48],
                TaxIndicator = x[49],
                TransactionCurrencyCode = x[50],
                IncentiveAmount = ParseDecimal(x[51]),
                NetAmount = ParseDecimal(x[52]),
                MiscellaneousCurrencyCode = x[53],
                MiscellaneousIncentiveAmount = ParseDecimal(x[54]),
                MiscellaneousNetAmount = ParseDecimal(x[55]),
                AlternateInvoicingCurrencyCode = x[56],
                AlternateInvoiceAmount = ParseDecimal(x[57]),
                InvoiceExchangeRate = ParseDecimal(x[58]),
                TaxVarianceAmount = ParseDecimal(x[59]),
                CurrencyVarianceAmount = ParseDecimal(x[60]),
                InvoiceLevelCharge = ParseDecimal(x[61]),
                InvoiceDueDate = ParseDateTime(x[62]),
                AlternateInvoiceNumber = x[63],
                StoreNumber = x[64],
                CustomerReferenceNumber = x[65],
                SenderName = x[66],
                SenderCompanyName = x[67],
                SenderAddressLine1 = x[68],
                SenderAddressLine2 = x[69],
                SenderCity = x[70],
                SenderState = x[71],
                SenderPostal = x[72],
                SenderCountry = x[73],
                ReceiverName = x[74],
                ReceiverCompanyName = x[75],
                ReceiverAddressLine1 = x[76],
                ReceiverAddressLine2 = x[77],
                ReceiverCity = x[78],
                ReceiverState = x[79],
                ReceiverPostal = x[80],
                ReceiverCountry = x[81],
                ThirdPartyName = x[82],
                ThirdPartyCompanyName = x[83],
                ThirdPartyAddressLine1 = x[84],
                ThirdPartyAddressLine2 = x[85],
                ThirdPartyCity = x[86],
                ThirdPartyState = x[87],
                ThirdPartyPostal = x[88],
                ThirdPartyCountry = x[89],
                SoldToName = x[90],
                SoldToCompanyName = x[91],
                SoldToAddressLine1 = x[92],
                SoldToAddressLine2 = x[93],
                SoldToCity = x[94],
                SoldToState = x[95],
                SoldToPostal = x[96],
                SoldToCountry = x[97],
                MiscellaneousAddressQual1 = x[98],
                MiscellaneousAddress1Name = x[99],
                MiscellaneousAddress1CompanyName = x[100],
                MiscellaneousAddress1AddressLine1 = x[101],
                MiscellaneousAddress1AddressLine2 = x[102],
                MiscellaneousAddress1City = x[103],
                MiscellaneousAddress1State = x[104],
                MiscellaneousAddress1Postal = x[105],
                MiscellaneousAddress1Country = x[106],
                MiscellaneousAddressQual2 = x[107],
                MiscellaneousAddress2Name = x[108],
                MiscellaneousAddress2CompanyName = x[109],
                MiscellaneousAddress2AddressLine1 = x[110],
                MiscellaneousAddress2AddressLine2 = x[111],
                MiscellaneousAddress2City = x[112],
                MiscellaneousAddress2State = x[113],
                MiscellaneousAddress2Postal = x[114],
                MiscellaneousAddress2Country = x[115],
                ShipmentDate = ParseDateTime(x[116]),
                ShipmentExportDate = ParseDateTime(x[117]),
                ShipmentImportDate = ParseDateTime(x[118]),
                EntryDate = ParseDateTime(x[119]),
                DirectShipmentDate = ParseDateTime(x[120]),
                ShipmentDeliveryDate = x[121],
                ShipmentReleaseDate = ParseDateTime(x[122]),
                CycleDate = x[123],
                EFTDate = x[124],
                ValidationDate = x[125],
                EntryPort = x[126],
                EntryNumber = x[127],
                ExportPlace = x[128],
                ShipmentValueAmount = ParseDecimal(x[129]),
                ShipmentDescription = x[130],
                EnteredCurrencyCode = x[131],
                CustomsNumber = x[132],
                ExchangeRate = ParseDecimal(x[133]),
                MasterAirWayBillNumber = x[134],
                EPU = x[135],
                EntryType = x[136],
                CPCCode = x[137],
                LineItemNumber = ParseInt(x[138]),
                GoodsDescription = x[139],
                EnteredValue = ParseDecimal(x[140]),
                DutyAmount = ParseDecimal(x[141]),
                Weight = x[142],
                UnitOfMeasure = x[143],
                ItemQuantity = ParseInt(x[144]),
                ItemQuantityUnitOfMeasure = x[145],
                ImportTaxId = x[146],
                DeclarationNumber = x[147],
                CarrierName = x[148],
                CCCDNumber = x[149],
                CycleNumber = x[150],
                ForeignTradeReferenceNumber = x[151],
                JobNumber = x[152],
                TransportMode = x[153],
                TaxType = x[154],
                TariffCode = x[155],
                TariffRate = ParseDecimal(x[156]),
                TariffTreatmentNumber = x[157],
                ContactName = x[158],
                ClassNumber = x[159],
                DocumentType = x[160],
                OfficeNumber = x[161],
                DocumentNumber = x[162],
                DutyValue = ParseDecimal(x[163]),
                TotalValueForDuty = ParseDecimal(x[164]),
                ExciseTaxAmount = ParseDecimal(x[165]),
                ExciseTaxRate = ParseDecimal(x[166]),
                GSTAmount = ParseDecimal(x[167]),
                GSTRate = ParseDecimal(x[168]),
                OrderInCouncil = x[169],
                OriginCountry = x[170],
                SIMAAccess = ParseDecimal(x[171]),
                TaxValue = ParseDecimal(x[172]),
                TotalCustomsAmount = ParseDecimal(x[173]),
                MiscellaneousLine1 = x[174],
                MiscellaneousLine2 = x[175],
                MiscellaneousLine3 = x[176],
                MiscellaneousLine4 = x[177],
                MiscellaneousLine5 = x[178],
                PayerRoleCode = x[179],
                MiscellaneousLine7 = x[180],
                MiscellaneousLine8 = x[181],
                MiscellaneousLine9 = x[182],
                MiscellaneousLine10 = x[183],
                MiscellaneousLine11 = x[184],
                DutyRate = ParseDecimal(x[185]),
                VATBasisAmount = ParseDecimal(x[186]),
                VATAmount = ParseDecimal(x[187]),
                VATRate = ParseDecimal(x[188]),
                OtherBasisAmount = ParseDecimal(x[189]),
                OtherAmount = ParseDecimal(x[190]),
                OtherRate = ParseDecimal(x[191]),
                OtherCustomsNumberIndicator = x[192],
                OtherCustomsNumber = x[193],
                CustomsOfficeName = x[194],
                PackageDimensionUnitOfMeasure = x[195],
                OriginalShipmentPackageQuantity = ParseInt(x[196]),
                CorrectedZone = x[197],
                TaxLawArticleNumber = x[198],
                TaxLawArticleBasisAmount = ParseDecimal(x[199]),
                OriginalTrackingNumber = x[200],
                ScaleWeightQuantity = ParseDecimal(x[201]),
                ScaleWeightUnitOfMeasure = x[202],
                RawDimensionsUnitOfMeasure = x[203],
                RawDimensions = x[204],
                BOL1 = x[205],
                BOL2 = x[206],
                BOL3 = x[207],
                BOL4 = x[208],
                BOL5 = x[209],
                PO1 = x[210],
                PO2 = x[211],
                PO3 = x[212],
                PO4 = x[213],
                PO5 = x[214],
                PO6 = x[215],
                PO7 = x[216],
                PO8 = x[217],
                PO9 = x[218],
                PO10 = x[219],
                NMFC = x[220],
                DetailClass = x[221],
                FreightSequenceNumber = ParseInt(x[222]),
                DeclaredFreightClass = x[223],
                EORINumber = x[224],
                DetailKeyedDim = x[225],
                DetailKeyedUnitOfMeasure = x[226],
                DetailKeyedBilledDimensions = x[227],
                DetailKeyedBilledUnitOfMeasure = x[228],
                OriginalServiceDescription = x[229],
            });
        }
        public static string GetFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                return reader.ReadToEnd();
            }
        }
    }

}