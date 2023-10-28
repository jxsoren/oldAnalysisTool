using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using AnalysisTool.Models;
using System.Text;
using System.Runtime.Serialization;
using CsvHelper.Configuration;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using System.IO.Compression;
using System.Reflection;

namespace AnalysisTool.Controllers
{
    public class DataController : Controller
    {
        public string dataFileFolder = "C:\\Users\\Administrator\\Desktop\\DataFiles\\";
        public string dataOutputFolder = "C:\\Users\\Administrator\\Desktop\\DataOutputFiles\\";

        public byte[] data;
        public byte[] archiveFile;

        public List<OutputModel> outputModels = new();
        public List<InputModel> im = new();
        public RateCalculations rateCalculations = new();
        public List<OutputModel> dataModels = new();

        public FormDataModel formData = new();
        public MarkupsAndSurchargesEtc markups = new();
        public string dlFilePath;
        public string outputFileName;

        public DateTime dateTime;

        public CsvConfiguration config = new(System.Globalization.CultureInfo.GetCultureInfo("en-US"))
        {
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            PrepareHeaderForMatch = args => Regex.Replace(args.Header.ToLower(), @"\s", string.Empty),
            
        };

        public void LoadDataToTable(FormDataModel fd)
        {
            markups.uspsDim = fd.uspsDim;
            markups.dhlDim = fd.dhlDim;
            //markups.uspsDiscountPercent = 1 - fd.uspsDiscountPercent;
            markups.uspsFCDiscountPercent = 1 - fd.uspsFCDiscountPercent;
            markups.uspsPMDiscountPercent = 1 - fd.uspsPMDiscountPercent;
            markups.uspsPSDiscountPercent = 1 - fd.uspsPSDiscountPercent;
            markups.uspsCanadaDiscountPercent = 1 - fd.uspsCanadaDiscountPercent;
            markups.uspsIntDiscountPercent = 1 - fd.uspsIntDiscountPercent;
            markups.fedexDim = fd.fedexDim;
            markups.fedexGeneralMarkupPercent = 1 + fd.fedexGeneralMarkupPercent;
            if (fd.fedexPOMarkupPercent > 0)
            {
                markups.fedexPOMarkupPercent = 1 + fd.fedexPOMarkupPercent;
            }
            else markups.fedexPOMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexPOMarkupPercent = 1 + fd.fedexPOMarkupPercent;
            if (fd.fedexSOMarkupPercent > 0)
            {
                markups.fedexSOMarkupPercent = 1 + fd.fedexSOMarkupPercent;
            }
            else markups.fedexSOMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexSOMarkupPercent = 1 + fd.fedexSOMarkupPercent;
            if (fd.fedexTDAMarkupPercent > 0)
            {
                markups.fedexTDAMarkupPercent = 1 + fd.fedexTDAMarkupPercent;
            }
            else markups.fedexTDAMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexTDAMarkupPercent = 1 + fd.fedexTDAMarkupPercent;
            if (fd.fedexTDMarkupPercent > 0)
            {
                markups.fedexTDMarkupPercent = 1 + fd.fedexTDMarkupPercent;
            }
            else markups.fedexTDMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexTDMarkupPercent = 1 + fd.fedexTDMarkupPercent;
            if (fd.fedexESMarkupPercent > 0)
            {
                markups.fedexESMarkupPercent = 1 + fd.fedexESMarkupPercent;
            }
            else markups.fedexESMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexESMarkupPercent = 1 + fd.fedexESMarkupPercent;
            if (fd.fedexGMarkupPercent > 0)
            {
                markups.fedexGMarkupPercent = 1 + fd.fedexGMarkupPercent;
            }
            else markups.fedexGMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexGMarkupPercent = 1 + fd.fedexGMarkupPercent;
            if (fd.fedexGHMarkupPercent > 0)
            {
                markups.fedexGHMarkupPercent = 1 + fd.fedexGHMarkupPercent;
            }
            else markups.fedexGHMarkupPercent = markups.fedexGeneralMarkupPercent;
            //markups.fedexGHMarkupPercent = 1 + fd.fedexGHMarkupPercent;
            markups.upsGeneralMarkupPercent = 1 + fd.upsGeneralMarkupPercent;
            if (fd.upsNDAMarkupPercent > 0)
            {
                markups.upsNDAMarkupPercent = 1 + fd.upsNDAMarkupPercent;
            }
            else markups.upsNDAMarkupPercent = markups.upsGeneralMarkupPercent;
            //markups.upsNDAMarkupPercent = 1 + fd.upsNDAMarkupPercent;
            if (fd.upsNDASMarkupPercent > 0)
            {
                markups.upsNDASMarkupPercent = 1 + fd.upsNDASMarkupPercent;
            }
            else markups.upsNDASMarkupPercent = markups.upsGeneralMarkupPercent;
            //markups.upsNDASMarkupPercent = 1 + fd.upsNDASMarkupPercent;
            if (fd.upsSDAMarkupPercent > 0)
            {
                markups.upsSDAMarkupPercent = 1 + fd.upsSDAMarkupPercent;
            }
            else markups.upsSDAMarkupPercent = markups.upsGeneralMarkupPercent;
            //markups.upsSDAMarkupPercent = 1 + fd.upsSDAMarkupPercent;
            if (fd.upsTDSMarkupPercent > 0)
            {
                markups.upsTDSMarkupPercent = 1 + fd.upsTDSMarkupPercent;
            }
            else markups.upsTDSMarkupPercent = markups.upsGeneralMarkupPercent;
            //markups.upsTDSMarkupPercent = 1 + fd.upsTDSMarkupPercent;
            if (fd.upsGMarkupPercent > 0)
            {
                markups.upsGMarkupPercent = 1 + fd.upsGMarkupPercent;
            }
            else markups.upsGMarkupPercent = markups.upsGeneralMarkupPercent;
            //markups.upsGMarkupPercent = 1 + fd.upsGMarkupPercent;
            markups.osmPSLWMarkupPercent = 1 + fd.osmPSLWMarkupPercent;
            markups.osmPSHWMarkupPercent = 1 + fd.osmPSHWMarkupPercent;
            markups.osmFuelSurcharge = 1 + fd.osmFuelSurcharge;
            markups.miPSHWMarkupPercent = 1 + fd.miPSHWMarkupPercent;
            markups.miPSLW1MarkupPercent = 1 + fd.miPSLW1MarkupPercent;
            markups.miPSLW2MarkupPercent = 1 + fd.miPSLW2MarkupPercent;
            markups.miPSLW3MarkupPercent = 1 + fd.miPSLW3MarkupPercent;
            markups.miPSLW4MarkupPercent = 1 + fd.miPSLW4MarkupPercent;
            markups.miFuelSurcharge = 1 + fd.miFuelSurcharge;
            markups.dhlGeneralMarkupPercent = 1 + fd.dhlGeneralMarkupPercent;
            if (fd.dhlMAXMarkupPercent > 0)
            {
                markups.dhlMAXMarkupPercent = 1 + fd.dhlMAXMarkupPercent;
            }
            else markups.dhlMAXMarkupPercent = markups.dhlGeneralMarkupPercent;
            //markups.dhlMAXMarkupPercent = 1 + fd.dhlMAXMarkupPercent;
            if (fd.dhlExpMarkupPercent > 0)
            {
                markups.dhlExpMarkupPercent = 1 + fd.dhlExpMarkupPercent;
            }
            else markups.dhlExpMarkupPercent = markups.dhlGeneralMarkupPercent;
            //markups.dhlExpMarkupPercent = 1 + fd.dhlExpMarkupPercent;
            if (fd.dhlExpPMarkupPercent > 0)
            {
                markups.dhlExpPMarkupPercent = 1 + fd.dhlExpPMarkupPercent;
            }
            else markups.dhlExpPMarkupPercent = markups.dhlGeneralMarkupPercent;
            //markups.dhlExpPMarkupPercent = 1 + fd.dhlExpPMarkupPercent;
            if (fd.dhlGroundMarkupPercent > 0)
            {
                markups.dhlGroundMarkupPercent = 1 + fd.dhlGroundMarkupPercent;
            }
            else markups.dhlGroundMarkupPercent = markups.dhlGeneralMarkupPercent;
            //markups.dhlGroundMarkupPercent = 1 + fd.dhlGroundMarkupPercent;
            if (fd.dhlGroundPMarkupPercent > 0)
            {
                markups.dhlGroundPMarkupPercent = 1 + fd.dhlGroundPMarkupPercent;
            }
            else markups.dhlGroundPMarkupPercent = markups.dhlGeneralMarkupPercent;
            //markups.dhlGroundPMarkupPercent = 1 + fd.dhlGroundPMarkupPercent;
            markups.dhleFuelSurcharge = fd.dhleFuelSurcharge;
            markups.dhleMM = fd.dhleMM;
            markups.osmMM = fd.osmMM;
            markups.miMM = fd.miMM;

            formData = fd;

            string fileName = formData.dataFile.FileName;

            formData.dataFilePath = dataFileFolder + fileName;

            using (var stream = System.IO.File.Create(formData.dataFilePath))
            {
                formData.dataFile.CopyTo(stream);
            }

            using (var fs = System.IO.File.Open(formData.dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, true))
                {
                    using (var csv = new CsvReader(textReader, config))
                    {
                        var a = csv.GetRecords<InputModel>();

                        foreach (var record in a)
                        {
                            List<decimal> dims = new();

                            if (record.length != null)
                            {
                                dims.Add((decimal)record.length);
                            }
                            else dims.Add(0);
                            if (record.width != null)
                            {
                                dims.Add((decimal)record.width);
                            }
                            else dims.Add(0);
                            if (record.height != null)
                            {
                                dims.Add((decimal)record.height);
                            }
                            else dims.Add(0);

                            decimal longest = dims.Max();
                            dims.Remove(longest);
                            decimal medium = dims.Max();
                            dims.Remove(medium);
                            decimal shortest = dims.Min();

                            record.length = (float)longest;
                            record.width = (float)medium;
                            record.height = (float)shortest;
                            im.Add(record);
                        }
                    }
                }
            }

            foreach (InputModel row in im)
            {
                OutputModel output = new();
                output.currentCarrier = row.carrier ?? "";
                output.currentService = row.service ?? "";
                output.packageID = row.packageID ?? "";
                if (row.cost != null && row.cost != "")
                {
                    if (row.cost.Contains("$"))
                    {
                        row.cost = row.cost.Replace("$", "");
                    }
                    output.shippingCost = decimal.Parse(row.cost);
                }
                else output.shippingCost = 0M;
                if (row.date != null)
                {
                    output.shippingDate = row.date;
                }
                else output.shippingDate = DateTime.Today;
                if (row.weight != null)
                {
                    output.originalWeightOz = (decimal)row.weight;
                }
                else output.originalWeightOz = 0M;
                output.lengthInches = (decimal)row.length;
                output.widthInches = (decimal)row.width;
                output.heightInches = (decimal)row.height;
                output.shipFromPostalCode = row.fromPostalCode ?? string.Empty;
                if (row.toPostalCode != null && row.toPostalCode != "")
                {
                    output.shipToPostalCode = row.toPostalCode;
                }
                else output.shipToPostalCode = "";
                if (row.country != null && row.country != "")
                {
                    output.shipToCountry = row.country.ToUpper();
                }
                else output.shipToCountry = "";
                if (row.state != null && row.state != "")
                {
                    output.shipToState = row.state.ToUpper();
                }
                else output.shipToState = "";
                if (row.zone != null)
                {
                    output.shipToZone = (decimal)row.zone;
                }
                else output.shipToZone = 0M;

                outputModels.Add(output);
            }

            ProcessData(outputModels);
        }

        public void ProcessData(List<OutputModel> lom)
        {
            dataModels = rateCalculations.RateEligibility(lom, markups);

            AnalysisOutput();
        }

        public void AnalysisOutput()
        {
            StringBuilder sb = new StringBuilder();

            string columnNames = "Customer,Package ID,Package Date,Current Carrier,Current Service,Ounces,Height,Length," +
                "Width,L+G Dimension,From Postal Code,To Postal Code,Destination State,Destination Country,Zone,FCPI Zone,PMI Zone," +
                "PMEI Zone,USPS Dim Weight,USPS Billing Weight,FedEx Dim Weight,FedEx Billing Weight,Rounded Pounds,Rounded Ounces,Cubic Tier," +
                "First Class Cost,Priority Mail Cost,Priority Mail Cubic Cost,Priority Mail Express Cost," +
                "Parcel Select Cost,Parcel Select Cubic Cost,First Class Int Cost,Priority Mail Int Cost,Priority Mail Express Int Cost," +
                "DHL MAX Cost,DHL Expedited Cost,DHL Expedited Plus Cost,DHL Ground Cost,DHL Ground Plus Cost," +
                "FedEx Standard Overnight Cost,FedEx Priority Overnight Cost,FedEx 2 Day AM Cost,FedEx 2 Day Cost," +
                "FedEx Express Saver Cost,FedEx Ground Cost,FedEx Ground Home Cost,MI PSLW Cost,MI PSHW Cost," +
                "OSM PSLW Cost,OSM PSHW Cost,UPS Next Day Air Cost,UPS Next Day Air Saver Cost,UPS 2nd Day Air Cost," +
                "UPS 3 Day Select Cost,UPS Ground Cost,Original Cost";

            sb.Append(columnNames);
            sb.Append("\r\n");

            for (var i = 0; i < dataModels.Count; i++)
            {
                sb.Append(formData.customerId + ',');
                sb.Append(dataModels[i].packageID + ",");
                sb.Append(dataModels[i].shippingDate.ToString() + ',');
                sb.Append(dataModels[i].currentCarrier.ToString() + ',');
                sb.Append(dataModels[i].currentService.ToString() + ',');
                sb.Append(dataModels[i].originalWeightOz.ToString() + ',');
                sb.Append(dataModels[i].heightInches.ToString() + ',');
                sb.Append(dataModels[i].lengthInches.ToString() + ',');
                sb.Append(dataModels[i].widthInches.ToString() + ',');
                sb.Append(dataModels[i].girth.ToString() + ',');
                sb.Append(dataModels[i].shipFromPostalCode.ToString() + ',');
                sb.Append(dataModels[i].shipToPostalCode.ToString() + ',');
                sb.Append(dataModels[i].shipToState.ToString() + ',');
                sb.Append(dataModels[i].shipToCountry.ToString() + ',');
                sb.Append(dataModels[i].shipToZone.ToString() + ',');
                sb.Append(dataModels[i].shipToFCPIZone + ',');
                sb.Append(dataModels[i].shipToPMIZone + ',');
                sb.Append(dataModels[i].shipToPMEIZone + ',');
                sb.Append(dataModels[i].uspsDimWeight.ToString() + ',');
                sb.Append(dataModels[i].uspsBillingWeight.ToString() + ',');
                sb.Append(dataModels[i].fedexDimWeight.ToString() + ",");
                sb.Append(dataModels[i].fedexBillingWeight.ToString() + ",");
                sb.Append(dataModels[i].roundedPounds.ToString() + ",");
                sb.Append(dataModels[i].roundedOunces.ToString() + ",");
                sb.Append(dataModels[i].cubicTier.ToString() + ',');
                sb.Append(dataModels[i].firstClassCost + ',');
                sb.Append(dataModels[i].priorityMailCost + ',');
                sb.Append(dataModels[i].priorityMailCubicCost + ',');
                sb.Append(dataModels[i].priorityMailExpressCost + ',');
                sb.Append(dataModels[i].parcelSelectCost + ',');
                sb.Append(dataModels[i].parcelSelectCubicCost + ',');
                sb.Append(dataModels[i].fcpiCost + ',');
                sb.Append(dataModels[i].pmiCost + ',');
                sb.Append(dataModels[i].pmeiCost + ',');
                sb.Append(dataModels[i].dhleMaxCost + ',');
                sb.Append(dataModels[i].dhleExpCost + ',');
                sb.Append(dataModels[i].dhleExpPCost + ',');
                sb.Append(dataModels[i].dhleGroundCost + ',');
                sb.Append(dataModels[i].dhleGroundPCost + ',');
                sb.Append(dataModels[i].fedexSOCost + ',');
                sb.Append(dataModels[i].fedexPOCost + ',');
                sb.Append(dataModels[i].fedexTDACost + ',');
                sb.Append(dataModels[i].fedexTDCost + ',');
                sb.Append(dataModels[i].fedexESCost + ',');
                sb.Append(dataModels[i].fedexGroundCost + ',');
                sb.Append(dataModels[i].fedexGHCost + ',');
                sb.Append(dataModels[i].mipslwCost + ',');
                sb.Append(dataModels[i].mipshwCost + ',');
                sb.Append(dataModels[i].osmpslwCost + ',');
                sb.Append(dataModels[i].osmpshwCost + ',');
                sb.Append(dataModels[i].upsNDACost + ',');
                sb.Append(dataModels[i].upsNDASCost + ',');
                sb.Append(dataModels[i].upsSDACost + ',');
                sb.Append(dataModels[i].upsTDSCost + ',');
                sb.Append(dataModels[i].upsGroundCost + ',');
                sb.Append(dataModels[i].shippingCost.ToString() + ',');

                sb.Append("\r\n");
            }

            var filename = formData.dataFile.FileName;
            string summaryTitle = filename.Remove(filename.Length - 4);

            formData.outputFile = dataOutputFolder + summaryTitle + " - analysis results.csv";
            dlFilePath = formData.outputFile;
            outputFileName = summaryTitle + " - analysis results " + DateTime.Now.ToString("MM-dd-yy hhmmsstt") + ".csv";
            
            data = Encoding.UTF8.GetBytes(sb.ToString()); // change "ns" back to "sb" when the zeros can be removed from the output file

            ByteArrayZip();
        }

        public void ByteArrayZip()
        {
            StringBuilder formParameters = new();

            PropertyInfo[] properties = typeof(FormDataModel).GetProperties();
            foreach (var param in properties)
            {
                if (param.Name != "outputFile" && param.Name != "dataFilePath" && param.Name != "dataFile")
                {
                    formParameters.AppendLine(param.Name + "," + param.GetValue(formData, null));
                }
            }

            InMemoryFile form = new()
            {
                FileName = outputFileName,
                Content = data
            };

            InMemoryFile dataFile = new()
            {
                FileName = "parameters for " + outputFileName,
                Content = Encoding.UTF8.GetBytes(formParameters.ToString())
            };

            List<InMemoryFile> files = new()
            {
                form,
                dataFile
            };



            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);

                        using var zipStream = zipArchiveEntry.Open();
                        zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }

                archiveFile = archiveStream.ToArray();
            }
        }

        class InMemoryFile
        {
            public string FileName { get; set; }
            public byte[] Content { get; set; }
        }
    }
}