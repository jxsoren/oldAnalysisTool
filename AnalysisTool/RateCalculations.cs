using System.Collections.Generic;
using System;
using AnalysisTool.Models;
using Microsoft.Data.Sqlite;
using AnalysisTool.Helpers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Routing.Matching;

namespace AnalysisTool
{
    public class RateCalculations
    {
        USPSRates usps = new();
        FedExRates fedex = new();
        UPSRates ups = new();
        DHLeRates dhle = new();
        MIRates mi = new();
        OSMRates osm = new();
        //int uspsDim = 166; // make this part of the UI
        //int fedexDim = 194;
        MarkupsAndSurchargesEtc markups = new();
        SqliteConnection physicalDbConnection;
        public SqliteConnection inMemoryDbConnection;

       

        public DateTime pPeakStart = new DateTime(DateTime.Now.Year - 1, 10, 01);
        public DateTime pPeakEnd = new DateTime(DateTime.Now.Year - 1, 12, 26);
        public DateTime cPeakStart = new DateTime(DateTime.Now.Year , 10, 01);
        public DateTime cPeakEnd = new DateTime(DateTime.Now.Year , 12, 26);

        public void CreateInMemDb()
        {
            var dbh = new DbHelpers();


            try
            {
                physicalDbConnection = dbh.GetPhysicalDbConnection();
                inMemoryDbConnection = dbh.GetInMemoryDbConnection();

                physicalDbConnection.BackupDatabase(inMemoryDbConnection, "main", "main");
                physicalDbConnection.Close();
            }
            catch
            {
                Console.WriteLine("DB creation error");
            }
        }

        public void InitializeConn()
        {
            usps.Initialize(inMemoryDbConnection);
            fedex.Initialize(inMemoryDbConnection);
            ups.Initialize(inMemoryDbConnection);
            dhle.Initialize(inMemoryDbConnection);
            mi.Initialize(inMemoryDbConnection);
            osm.Initialize(inMemoryDbConnection);
        }

        public List<OutputModel> RateEligibility(List<OutputModel> lom, MarkupsAndSurchargesEtc m)
        {
            CreateInMemDb();

            InitializeConn();

            markups = m;

            foreach (var row in lom)
            {
                row.cubicSize = row.lengthInches * row.widthInches * row.heightInches;
                row.cubicTier = CalculateCube(row.lengthInches, row.widthInches, row.heightInches);
                row.uspsDimWeight = (int)Math.Ceiling((row.cubicSize) / markups.uspsDim);
                row.roundedPounds = (int)Math.Ceiling(row.originalWeightOz / 16);
                row.roundedOunces = (int)Math.Ceiling(row.originalWeightOz);
                row.girth = row.lengthInches + (row.widthInches * 2) + (row.heightInches * 2);
                if (row.cubicSize > 1728)
                {
                    row.uspsBillingWeight = Math.Max(row.uspsDimWeight, row.roundedPounds);
                }
                else row.uspsBillingWeight = row.roundedPounds;
                row.fedexDimWeight = (int)Math.Ceiling((row.cubicSize) / markups.fedexDim);
                row.fedexBillingWeight = Math.Max(row.fedexDimWeight, row.roundedPounds);
                row.upsDimWeight = (int)Math.Ceiling((row.cubicSize) / markups.uspsDim);
                row.upsBillingWeight = Math.Max(row.upsDimWeight, row.roundedPounds);

                if (row.shipToCountry != null && row.shipToCountry != "")
                {
                    if (row.shipToCountry.ToUpper() == "US")
                    {
                        DomesticRates(row);
                    }
                    else
                    {
                        InternationalRates(row);
                    }
                }
                else
                {
                    //  ¯\_(ツ)_/¯
                }
            }

            inMemoryDbConnection.Close();

            return lom;
        }

        public void DomesticRates(OutputModel row)
        {
            int uspsWeightOz;
            decimal MM = 0M;
            decimal mm = 3M;
            decimal dhleMM = 0M;
            decimal dhleMMplus = 0M;
            decimal miMMlw = 0M;
            decimal miMMhw = 0M;
            decimal osmMMlw = 0M;
            decimal osmMMhw = 0M;

            //row.cubicSize = (row.lengthInches * row.heightInches * row.widthInches);
            //row.cubicTier = CalculateCube(row.lengthInches, row.widthInches, row.heightInches);
            //row.uspsDimWeight = (int)Math.Ceiling((row.cubicSize) / markups.uspsDim);
            //row.roundedPounds = (int)Math.Ceiling(row.originalWeightOz / 16);
            //row.roundedOunces = (int)Math.Ceiling(row.originalWeightOz);
            //row.girth = row.lengthInches + (row.widthInches * 2) + (row.heightInches * 2);
            //if (row.cubicSize > 1728)
            //{
            //    row.uspsBillingWeight = Math.Max(row.uspsDimWeight, row.roundedPounds);
            //}
            //else row.uspsBillingWeight = row.roundedPounds;
            //row.fedexDimWeight = (int)Math.Ceiling((row.cubicSize) / markups.fedexDim);
            //row.fedexBillingWeight = Math.Max(row.fedexDimWeight, row.roundedPounds);
            //row.upsDimWeight = (int)Math.Ceiling((row.cubicSize) / markups.uspsDim);
            //row.upsBillingWeight = Math.Max(row.upsDimWeight, row.roundedPounds);

            row.dhlDimWeight = Math.Ceiling(((row.lengthInches * row.widthInches * row.heightInches) / markups.dhlDim) * 16);
            row.dhlBillingWeight = Math.Max(row.dhlDimWeight, row.roundedOunces);

            decimal dhlFuelSurchargeBasic = markups.dhleFuelSurcharge / 16 * row.roundedOunces;
            decimal dhlFuelSurchargePlus = markups.dhleFuelSurcharge * row.roundedPounds;

            switch (row.uspsBillingWeight)
            {
                case 1M:
                    MM = (.00482143M * ((int)row.shipToZone ^ 2)) - (0.0130357M * row.shipToZone) + .499464M;
                    break;
                case 2M:
                    MM = (.01125M * ((int)row.shipToZone ^ 2)) - (0.0435119M * row.shipToZone) + .557679M;
                    break;
                case 3M:
                    MM = (.02125M * ((int)row.shipToZone ^ 2)) - (0.0794643M * row.shipToZone) + .589464M;
                    break;
                case 4M:
                    MM = (.0225M * ((int)row.shipToZone ^ 2)) - (0.0532143M * row.shipToZone) + .540714M;
                    break;
                case 5M:
                    MM = (.0282738M * ((int)row.shipToZone ^ 2)) - (0.0710119M * row.shipToZone) + .559821M;
                    break;
                case 6M:
                    MM = (.0358929M * ((int)row.shipToZone ^ 2)) - (0.107202M * row.shipToZone) + .623393M;
                    break;
                case 7M:
                    MM = (.0394643M * ((int)row.shipToZone ^ 2)) - (0.116488M * row.shipToZone) + .651607M;
                    break;
                case 8M:
                    MM = (.039881M * ((int)row.shipToZone ^ 2)) - (0.0946429M * row.shipToZone) + .621429M;
                    break;
                case 9M:
                    MM = (.0441667M * ((int)row.shipToZone ^ 2)) - (0.113929M * row.shipToZone) + .683929M;
                    break;
                case 10M:
                    MM = (.0486905M * ((int)row.shipToZone ^ 2)) - (0.133929M * row.shipToZone) + .728571M;
                    break;
                default:
                    MM = 0M;
                    break;
            }

            if (markups.dhleMM)
            {
                dhleMMplus = MM;
                dhleMM = mm;
            }

            if (markups.miMM)
            {
                miMMhw = MM;
                miMMlw = mm;
            }

            if (markups.osmMM)
            {
                osmMMhw = MM;
                osmMMlw = mm;
            }
            

            var year = (DateTime)row.shippingDate;
            var y = year.Year;
            string t; // current year or previous year
            string z; // peak season or standard
            if (y == DateTime.Now.Year)
            {
                t = "C"; // current year
                if (row.shippingDate >= cPeakStart && row.shippingDate < cPeakEnd)
                {
                    z = "P";
                }
                else
                {
                    z = "S";
                }
            }
            else
            {
                t = "P"; // previous year
                if (row.shippingDate >= pPeakStart && row.shippingDate < pPeakEnd)
                {
                    z = "P";
                }
                else
                {
                    z = "S";
                }
            }
            
            

            if (row.originalWeightOz > 0 && row.originalWeightOz.ToString() != null)
            {
                if (row.originalWeightOz < 16M)
                {
                    if (row.originalWeightOz > 15M)
                    {
                        uspsWeightOz = 16;
                    }
                    else
                    {
                        uspsWeightOz = (int)row.roundedOunces;
                    }

                    if (row.lengthInches <= 22M && row.widthInches <= 18M && row.heightInches <= 15M && row.girth <= 108M)
                    {
                        var firstClassCost = Math.Round(usps.FC($"usps{t}{z}FC", row.shipToZone.ToString(), uspsWeightOz.ToString()) * markups.uspsFCDiscountPercent, 2);
                        if (firstClassCost > 0)
                        {
                            row.firstClassCost = firstClassCost.ToString();
                        }
                        else row.firstClassCost = null;
                        //row.firstClassCost = Math.Round(usps.FC($"usps{t}{z}FC", row.shipToZone.ToString(), uspsWeightOz.ToString()) * markups.uspsFCDiscountPercent,2);

                        if (row.shipToZone < 9 && row.shipToState != "HI" && row.shipToState != "AK" && row.shipToState != "PR")
                        {
                            var osmpslwCost = osm.PSLW($"osm{t}PSLW", row.shipToZone.ToString(), uspsWeightOz.ToString());
                            if (osmpslwCost > 0)
                            {
                                row.osmpslwCost = Math.Round((osmpslwCost * markups.osmFuelSurcharge + (osmpslwCost * osmMMlw)) * markups.osmPSLWMarkupPercent, 2).ToString();
                            }
                            else row.osmpslwCost = null;
                        }
                    }

                    if (row.lengthInches <= 16M && row.widthInches <= 13 && row.heightInches <= 10 && row.girth <= 36M && row.shipToZone < 9 && 
                        row.shipToState != "HI" && row.shipToState != "AK" && row.shipToState != "PR")
                    {
                        decimal miMarkup;
                        if (uspsWeightOz < 5)
                        {
                            miMarkup = markups.miPSLW1MarkupPercent;
                        }
                        else if (uspsWeightOz < 9)
                        {
                            miMarkup = markups.miPSLW2MarkupPercent;
                        }
                        else if (uspsWeightOz < 13)
                        {
                            miMarkup = markups.miPSLW3MarkupPercent;
                        }
                        else
                        {
                            miMarkup = markups.miPSLW4MarkupPercent;
                        }
                        var mipslwCost = mi.PSLW($"mi{t}PSLW", row.shipToZone.ToString(), uspsWeightOz.ToString());
                        if (mipslwCost > 0)
                        {
                            row.mipslwCost = Math.Round((mipslwCost * markups.miFuelSurcharge + (mipslwCost * miMMlw)) * miMarkup, 2).ToString();
                        }
                        else row.mipslwCost = null;
                    }

                }

                if (row.dhlBillingWeight <= 400M && row.girth <= 50M && row.lengthInches <= 27M && row.widthInches <= 17M && row.shipToZone < 9 && 
                    row.shipToState != "HI" && row.shipToState != "AK" && row.shipToState != "PR" && row.shipToState != "GU" && row.shipToState != "AA" && 
                    row.shipToState != "AE" && row.shipToState != "AP" && row.shipToState != "VI" && row.shipToState != "AS" && row.shipToState != "FM" && 
                    row.shipToState != "MH" && row.shipToState != "PW")
                {
                    var dhleMaxCost = Math.Round((dhle.Max($"dhle{t}Max", row.shipToZone.ToString(), row.dhlBillingWeight.ToString()) + dhlFuelSurchargePlus) * markups.dhlMAXMarkupPercent, 2);
                    if (dhleMaxCost > 0)
                    {
                        row.dhleMaxCost = dhleMaxCost.ToString();
                    }

                    if (row.originalWeightOz < 16 && row.dhlBillingWeight <= 16)
                    {
                        var dhleExpCost = dhle.Exp($"dhle{t}Exp", row.shipToZone.ToString(), row.dhlBillingWeight.ToString());
                        if (dhleExpCost > 0)
                        {
                            dhleExpCost += dhlFuelSurchargePlus + (dhleExpCost * dhleMM);
                            row.dhleExpCost = Math.Round(dhleExpCost * markups.dhlExpMarkupPercent, 2).ToString();
                        }
                        else row.dhleExpCost = null;

                        var dhleGroundCost = dhle.Ground($"dhle{t}Ground", row.shipToZone.ToString(), row.dhlBillingWeight.ToString());
                        if (dhleGroundCost > 0)
                        {
                            dhleGroundCost += dhlFuelSurchargePlus + (dhleGroundCost * dhleMM);
                            row.dhleGroundCost = Math.Round(dhleGroundCost * markups.dhlGroundMarkupPercent, 2).ToString();
                        }
                        else row.dhleGroundCost = null;
                    }
                    else
                    {
                        var dhleExpPCost = dhle.ExpP($"dhle{t}ExpP", row.shipToZone.ToString(), row.dhlBillingWeight.ToString());
                        if (dhleExpPCost > 0)
                        {
                            row.dhleExpPCost = Math.Round((dhleExpPCost + dhlFuelSurchargePlus + dhleMMplus) * markups.dhlExpPMarkupPercent, 2).ToString();
                        }
                        else row.dhleExpPCost = null;

                        var dhleGroundPCost = dhle.GroundP($"dhle{t}GroundP", row.shipToZone.ToString(), row.dhlBillingWeight.ToString());
                        if (dhleGroundPCost > 0)
                        {
                            row.dhleGroundPCost = Math.Round((dhleGroundPCost + dhlFuelSurchargePlus + dhleMMplus) * markups.dhlGroundPMarkupPercent, 2).ToString();
                        }
                        else row.dhleGroundPCost = null;
                    }
                }

                if (row.cubicTier <= .5M && row.cubicTier > 0 && row.lengthInches <= 18 && row.uspsBillingWeight <= 20)
                {
                    var priorityMailCubicCost = Math.Round(usps.PMCubic($"usps{t}{z}PM", row.shipToZone.ToString(), row.cubicTier.ToString()) * markups.uspsPMDiscountPercent, 2);
                    if (priorityMailCubicCost > 0)
                    {
                        row.priorityMailCubicCost = priorityMailCubicCost.ToString();
                    }
                    else row.priorityMailCubicCost = null;
                }

                if (row.uspsBillingWeight <= 70M)
                {
                    if (row.girth <= 108M)
                    {
                        var priorityMailCost = Math.Round(usps.PM($"usps{t}{z}PM", row.shipToZone.ToString(), row.uspsBillingWeight.ToString()) * markups.uspsPMDiscountPercent, 2);
                        if (priorityMailCost > 0)
                        {
                            if (row.cubicTier > 2)
                            {
                                row.priorityMailCost = (priorityMailCost + 25).ToString();
                            }
                            else row.priorityMailCost = priorityMailCost.ToString();

                            if (row.lengthInches > 22)
                            {
                                row.priorityMailCost += 4;

                                if (row.lengthInches > 30)
                                {
                                    row.priorityMailCost += 11;
                                }
                            }
                        }
                        else row.priorityMailCost = null;

                        var priorityMailExpressCost = Math.Round(usps.PME($"usps{t}{z}PME", row.shipToZone.ToString(), row.uspsBillingWeight.ToString()), 2);
                        if (priorityMailExpressCost > 0)
                        {
                            if (row.cubicTier > 2)
                            {
                                row.priorityMailExpressCost += 25;
                            }
                            else row.priorityMailExpressCost = priorityMailExpressCost.ToString();

                            if (row.lengthInches > 22)
                            {
                                row.priorityMailExpressCost += 4;

                                if (row.lengthInches > 30)
                                {
                                    row.priorityMailExpressCost += 11;
                                }
                            }
                        }
                        else row.priorityMailExpressCost = null;

                        if (row.shipToZone < 9 && row.shipToState != "AK" && row.shipToState != "HI" && row.shipToState != "PR")
                        {
                            var osmpshwCost = osm.PSHW($"osm{t}PSHW", row.shipToZone.ToString(), row.uspsBillingWeight.ToString());
                            if (osmpshwCost > 0)
                            {
                                row.osmpshwCost = Math.Round(((osmpshwCost * markups.osmFuelSurcharge) + osmMMhw) * markups.osmPSHWMarkupPercent, 2).ToString();
                            }
                            else row.osmpshwCost = null;
                        }
                        
                    }

                    if (row.lengthInches <= 18 && row.shipToZone < 9 && row.uspsBillingWeight <= 20)
                    {
                        var parcelSelectCubicCost = Math.Round(usps.PSCubic($"usps{t}{z}PSCubic", row.shipToZone.ToString(), row.cubicTier) * markups.uspsPSDiscountPercent, 2);
                        if (parcelSelectCubicCost > 0)
                        {
                            row.parcelSelectCubicCost = parcelSelectCubicCost.ToString();
                        }
                        else row.parcelSelectCubicCost = null;
                    }

                    if (row.girth <= 108 && row.uspsBillingWeight <= 70M)
                    {
                        var parcelSelectCost = Math.Round(usps.PS($"usps{t}{z}PS", row.shipToZone.ToString(), row.uspsBillingWeight.ToString()) * markups.uspsPSDiscountPercent, 2);
                        
                        if (parcelSelectCost > 0)
                        {
                            if (row.cubicTier > 2)
                            {
                                row.parcelSelectCost = (parcelSelectCost + 15).ToString();
                            }
                            else row.parcelSelectCost = parcelSelectCost.ToString();

                            if (row.lengthInches > 22)
                            {
                                row.parcelSelectCost += 4;

                                if (row.lengthInches > 30)
                                {
                                    row.parcelSelectCost += 3;
                                }
                            }
                        }
                        else row.parcelSelectCost = null;
                    }

                    if (row.girth <= 165 && row.lengthInches <= 16 && row.widthInches <= 13 && row.heightInches <= 10 && row.shipToState != "AK" && 
                        row.shipToState != "HI" && row.shipToState != "PR")
                    {
                        var mipshwCost = mi.PSHW($"mi{t}PSHW", row.shipToZone.ToString(), row.uspsBillingWeight.ToString());
                        if (mipshwCost > 0)
                        {
                            row.mipshwCost = Math.Round(((mipshwCost * markups.miFuelSurcharge) + miMMhw) * markups.miPSHWMarkupPercent, 2).ToString();
                        }
                        else row.mipshwCost = null;
                    }
                }

                if (row.upsBillingWeight <= 150M && row.girth <= 165 && row.shipToZone < 9 && row.lengthInches <= 108)
                {
                    var upsNDACost = Math.Round(ups.NDA($"ups{t}NDA", row.shipToZone.ToString(), row.upsBillingWeight.ToString()) * markups.upsNDAMarkupPercent, 2);
                    if (upsNDACost > 0)
                    {
                        row.upsNDACost = upsNDACost.ToString();
                    }
                    else row.upsNDACost = null;

                    var upsNDASCost = Math.Round(ups.NDAS($"ups{t}NDAS", row.shipToZone.ToString(), row.upsBillingWeight.ToString()) * markups.upsNDASMarkupPercent, 2);
                    if (upsNDASCost > 0)
                    {
                        row.upsNDASCost = upsNDASCost.ToString();
                    }
                    else row.upsNDASCost = null;

                    var upsSDACost = Math.Round(ups.SDA($"ups{t}SDA", row.shipToZone.ToString(), row.upsBillingWeight.ToString()) * markups.upsSDAMarkupPercent, 2);
                    if (upsSDACost > 0)
                    {
                        row.upsSDACost = upsSDACost.ToString();
                    }
                    else row.upsSDACost = null;

                    var upsTDSCost = Math.Round(ups.TDS($"ups{t}TDS", row.shipToZone.ToString(), row.upsBillingWeight.ToString()) * markups.upsTDSMarkupPercent, 2);
                    if (upsTDSCost > 0)
                    {
                        row.upsTDSCost = upsTDSCost.ToString();
                    }  
                    else row.upsTDSCost = null;

                    var upsGroundCost = Math.Round(ups.Ground($"ups{t}Ground", row.shipToZone.ToString(), row.upsBillingWeight.ToString()) * markups.upsGMarkupPercent, 2);
                    if (upsGroundCost > 0)
                    {
                        row.upsGroundCost = upsGroundCost.ToString();
                    }
                    else row.upsGroundCost = null;
                }

                if (row.fedexBillingWeight <= 150M && row.girth <= 165 && row.shipToZone < 9)
                {
                    if (row.lengthInches <= 108)
                    {
                        var fedexPOCost = Math.Round(fedex.PO($"fedex{t}PO", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexPOMarkupPercent, 2);
                        if (fedexPOCost > 0)
                        {
                            row.fedexPOCost = fedexPOCost.ToString();
                        }
                        else row.fedexPOCost = null;

                        var fedexGroundCost = Math.Round(fedex.Ground($"fedex{t}Ground", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexGMarkupPercent, 2);
                        if (fedexGroundCost > 0)
                        {
                            row.fedexGroundCost = fedexGroundCost.ToString();
                        }
                        else row.fedexGroundCost = null;

                        var fedexGHCost = Math.Round(fedex.GH($"fedex{t}GroundH", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexGHMarkupPercent, 2);
                        if (fedexGHCost > 0)
                        {
                            row.fedexGHCost = fedexGHCost.ToString();
                        }
                        else row.fedexGHCost = null;
                    }

                    if (row.lengthInches <= 119)
                    {
                        var fedexSOCost = Math.Round(fedex.SO($"fedex{t}SO", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexSOMarkupPercent, 2);
                        if (fedexSOCost > 0)
                        {
                            row.fedexSOCost = fedexSOCost.ToString();
                        }
                        else row.fedexSOCost = null;

                        var fedexTDACost = Math.Round(fedex.TDA($"fedex{t}TDA", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexTDAMarkupPercent, 2);
                        if (fedexTDACost > 0)
                        {
                            row.fedexTDACost = fedexTDACost.ToString();
                        }
                        else row.fedexTDACost = null;

                        var fedexTDCost = Math.Round(fedex.TD($"fedex{t}TD", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexTDMarkupPercent, 2);
                        if (fedexTDCost > 0)
                        {
                            row.fedexTDCost = fedexTDCost.ToString();
                        }
                        else row.fedexTDCost = null;

                        var fedexESCost = Math.Round(fedex.ES($"fedex{t}ES", row.shipToZone.ToString(), row.fedexBillingWeight.ToString()) * markups.fedexESMarkupPercent, 2);
                        if (fedexESCost > 0)
                        {
                            row.fedexESCost = fedexESCost.ToString();
                        }
                        else row.fedexESCost = null;
                    }
                }
            }
            else
            {
                // log an error for that row
            }
        }

        public void InternationalRates(OutputModel row)
        {
            USPSIntInfo intInfo = usps.USPSInt(row.shipToCountry);
            //row.roundedPounds = (int)Math.Ceiling(row.originalWeightOz / 16);
            decimal lwh = row.lengthInches + row.widthInches + row.heightInches;
            //row.girth = row.lengthInches + (row.widthInches * 2) + (row.heightInches * 2);
            //row.roundedOunces = (int)Math.Ceiling(row.originalWeightOz);
            row.shipToFCPIZone = intInfo.fcpiZone;
            row.shipToPMIZone = intInfo.pmiZone;
            row.shipToPMEIZone = intInfo.pmeiZone;
            var year = (DateTime)row.shippingDate;
            var y = year.Year;
            string t; // current year or previous year
            string z; // peak season or standard
            if (y == DateTime.Now.Year)
            {
                t = "C"; // current year
                if (row.shippingDate >= cPeakStart && row.shippingDate < cPeakEnd)
                {
                    z = "P";
                }
                else
                {
                    z = "S";
                }
            }
            else
            {
                t = "P"; // previous year
                if (row.shippingDate >= pPeakStart && row.shippingDate < pPeakEnd)
                {
                    z = "P";
                }
                else
                {
                    z = "S";
                }
            }

            if (row.roundedOunces <= 64 && row.lengthInches >= 6 && row.widthInches >= 4 && 
                row.lengthInches <= 24 && lwh <= 36 && intInfo.fcpiZone != "0")
            {
                decimal fcpi = usps.FCPI($"usps{t}{z}FCPI", row.shipToFCPIZone.ToString(), row.roundedOunces.ToString());
                if (fcpi > 0)
                {
                    if (row.shipToCountry == "CA")
                    {
                        fcpi *= markups.uspsCanadaDiscountPercent;
                        row.fcpiCost = Math.Round(fcpi, 2).ToString();
                    }
                    else
                    {
                        fcpi *= markups.uspsIntDiscountPercent;
                        row.fcpiCost = Math.Round(fcpi, 2).ToString();
                    }
                }
                else row.fcpiCost = null;
            }

            if (intInfo.pmiWeight > 0 && row.uspsBillingWeight <= intInfo.pmiWeight && row.girth <= 108 && intInfo.pmiZone != "0")
            {
                decimal pmi = usps.PMI($"usps{t}{z}PMI", row.shipToPMIZone.ToString(), row.uspsBillingWeight.ToString());
                if (pmi > 0)
                {
                    if (row.uspsBillingWeight <= 20)
                    {
                        if (row.shipToCountry == "CA")
                        {
                            pmi *= markups.uspsCanadaDiscountPercent;
                            row.pmiCost = Math.Round(pmi, 2).ToString();
                        }
                        else
                        {
                            pmi *= markups.uspsIntDiscountPercent;
                            row.pmiCost = Math.Round(pmi, 2).ToString();
                        }
                    }
                    else
                    {
                        row.pmiCost = Math.Round(pmi, 2).ToString();
                    }
                }
                else row.pmiCost = null;
            }

            if (intInfo.pmeiWeight > 0 && row.uspsBillingWeight <= intInfo.pmeiWeight && row.girth <= 79 && intInfo.pmeiZone != "0")
            {
                decimal pmei = usps.PMI($"usps{t}{z}PMEI", row.shipToPMEIZone.ToString(), row.uspsBillingWeight.ToString());
                if (pmei > 0)
                {
                    if (row.uspsBillingWeight <= 10)
                    {
                        if (row.shipToCountry == "CA")
                        {
                            pmei *= markups.uspsCanadaDiscountPercent;
                            row.pmeiCost = Math.Round(pmei, 2).ToString();
                        }
                        else
                        {
                            pmei *= markups.uspsIntDiscountPercent;
                            row.pmeiCost = Math.Round(pmei, 2).ToString();
                        }
                    }
                    else
                    {
                        row.pmeiCost = Math.Round(pmei, 2).ToString();
                    }
                }
                else row.pmeiCost = null;
            }

            if (row.shipToFCPIZone == "0")
            {
                row.shipToFCPIZone = null;
            }

            if (row.shipToPMIZone == "0")
            {
                row.shipToPMIZone = null;
            }

            if (row.shipToPMEIZone == "0")
            {
                row.shipToPMEIZone = null;
            }
        }

        public decimal CalculateCube(decimal l, decimal w, decimal h)
        {
            decimal len = RoundDim(l);
            decimal wid = RoundDim(w);
            decimal hei = RoundDim(h);
            
            decimal cube = len * wid * hei;

            return Math.Ceiling((cube / 1728) * 10) / 10;
        }

        public decimal RoundDim(decimal d)
        {
            decimal r;
            if (d - (int)d >= 0M && d - (int)d < .25M)
            {
                r = Math.Round(d, 0);
            }
            else if (d - (int)d >= .25M && d - (int)d < .5M)
            {
                r = Math.Round(d, 0) + .25M;
            }
            else if (d - (int)d >= .5M && d - (int)d < .75M)
            {
                r = Math.Round(d, 0) + .5M;
            }
            else
            {
                r = Math.Round(d, 0) + .75M;
            }

            return r;
        }
    }
}
