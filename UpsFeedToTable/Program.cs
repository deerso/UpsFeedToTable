using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var testFileName = @"\\10.0.1.10\feeds\ups\Invoice_0000007V013V012_010712.csv";

            var rows = File.ReadLines(testFileName).Select(x => x.Split(new[] {','})).Select(x => new EDI_Data 
            {
                
                RecipientNumber = x[1],
                AccountNumber = x[2],
                AccountCountry = x[3],
                InvoiceDate = DateTime.Parse(x[4]),
                InvoiceNumber = x[5],
                InvoiceTypeCode = x[6],
                InvoiceTypeDetailCode = x[7],
                AccountTaxId = x[8],
                InvoiceCurrencyCode = x[9],
                InvoiceAmount = decimal.Parse(x[10]),
                TransactionDate = DateTime.Parse(x[11]),
                PickupRecordNumber = x[12],
                LeadShipmentNumber = x[13],
                WorldEaseNumber =  x[14],
                ShipmentReferenceNumber1 = x[15],
                ShipmentReferenceNumber2 = x[16],
                BillOptionCode = x[17],
                PackageQuantity = int.Parse(x[18]),
                OversizeQuantity = int.Parse(x[19]),
                TrackingNumber = x[20],
                PackageReferenceNumber1 = x[21],
                PackageReferenceNumber2 = x[22],
                PackageReferenceNumber3 = x[23],
                PackageReferenceNumber4 = x[24],
                PackageReferenceNumber5 = x[25],
                EnteredWeight = decimal.Parse(x[26]),
                EnteredWeightUnitOfMeasure = x[27],
                BilledWeight = decimal.Parse(x[28]),
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
                ChargedUnitQuantity = int.Parse(x[46]),
                BasisCurrencyCode = x[47],
                BasisValue = x[48],
                TaxIndicator = x[49],
                TransactionCurrencyCode = x[50],
                IncentiveAmount = decimal.Parse(x[51]),
                NetAmount = decimal.Parse(x[52]),
                MiscellaneousCurrencyCode = x[53],
                MiscellaneousIncentiveAmount = decimal.Parse(x[54]),
                MiscellaneousNetAmount = decimal.Parse(x[55]),
                AlternateInvoicingCurrencyCode = x[56],
                AlternateInvoiceAmount = decimal.Parse(x[57]),
                InvoiceExchangeRate = decimal.Parse(x[58]),
                TaxVarianceAmount = decimal.Parse(x[59]),
                CurrencyVarianceAmount = decimal.Parse(x[60]),
                InvoiceLevelCharge = decimal.Parse(x[61]),
                InvoiceDueDate = DateTime.Parse(x[62]),
                AlternateInvoiceNumber = x[63],
                StoreNumber = x[64],
                CustomerReferenceNumber = x[65],
                SenderName = x[66],
                SenderCompanyName = x[67],
                SenderAddressLine1 = x[68],
                SenderAddressLine2 = x[69],
                SenderCity= x[70],
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
                ShipmentDate = DateTime.Parse(x[116]),
                ShipmentExportDate = DateTime.Parse(x[117]),
                ShipmentImportDate = DateTime.Parse(x[118]),
                EntryDate = DateTime.Parse(x[119]),
                DirectShipmentDate = DateTime.Parse(x[120]),
                ShipmentDeliveryDate =x[121],
                ShipmentReleaseDate = DateTime.Parse(x[122]),
                CycleDate = x[123],
                EFTDate = x[124],
                ValidationDate = x[125],
                EntryPort = x[128],
                EntryNumber = x[127],
                ExportPlace = x[128],
                ShipmentValueAmount = decimal.Parse(x[129]),
                ShipmentDescription = x[130],
                EnteredCurrencyCode = x[131],
                CustomsNumber = x[132],
                ExchangeRate = decimal.Parse(x[133]),
                MasterAirWayBillNumber = x[134],
                EPU = x[135],
                EntryType = x[136],
                CPCCode = x[137],
                LineItemNumber = int.Parse(x[128]),
                GoodsDescription = x[128],
                EnteredValue = decimal.Parse(x[128]),
                DutyAmount = decimal.Parse(x[128]),
                Weight = x[128],
                UnitOfMeasure = x[128],
                ItemQuantity = int.Parse(x[128]),
                ItemQuantityUnitOfMeasure = x[128],
                ImportTaxId = x[128],
                DeclarationNumber = x[128],
                CarrierName = x[128],
                CCCDNumber = x[128],
                CycleNumber = x[128],
                ForeignTradeReferenceNumber = x[128],
                JobNumber = x[128],
                TransportMode = x[128],
                TaxType = x[128],
                TariffCode = x[128],
                TariffRate = decimal.Parse(x[128]),
            });

            Console.WriteLine(rows.FirstOrDefault().Dump());
        }
    }

    public class KyleFileReader
    {
        public KyleFileReader()
        {
            
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
