using AnalysisTool.Models;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;

namespace AnalysisTool
{
    public class ColumnMapping
    {
        RateTables rateTables = new();
        Calculations calculations = new();
        List<DataModel> analyzedData = new();

        public List<DataModel> TableToDataModel(DataTable dataToAnalyze, FormDataModel formData)
        {
            rateTables.ProcessRateTables(formData);
            var j = 0;
            foreach (var row in dataToAnalyze.Rows)
            {
                DataModel dataModel = new DataModel();

                try
                {
                    dataModel.packageDate = DateTime.Parse(dataToAnalyze.Rows[j][0].ToString());
                    dataModel.rates = rateTables.DetermineRateTables(dataModel.packageDate);
                }
                catch
                {
                    dataModel.packageDate = DateTime.Now;
                    dataModel.rates = rateTables.DetermineRateTables(dataModel.packageDate);
                }
                try
                {
                    dataModel.currentCarrier = dataToAnalyze.Rows[j][1].ToString();
                }
                catch
                {
                    dataModel.currentCarrier = "";
                }
                try
                {
                    dataModel.currentService = dataToAnalyze.Rows[j][2].ToString();
                }
                catch
                {
                    dataModel.currentService = "";
                }
                try
                {
                    dataModel.totalWeightOunces = decimal.Parse(dataToAnalyze.Rows[j][3].ToString());
                }
                catch
                {
                    dataModel.totalWeightOunces = 0M;
                }
                try
                {
                    if (dataToAnalyze.Rows[j][4].ToString() != null && dataToAnalyze.Rows[j][4].ToString() != "")
                    {
                        List<decimal> dims = new List<decimal>();
                        decimal l = decimal.Parse(dataToAnalyze.Rows[j][4].ToString());
                        dims.Add(l);
                        decimal w = decimal.Parse(dataToAnalyze.Rows[j][5].ToString());
                        dims.Add(w);
                        decimal h = decimal.Parse(dataToAnalyze.Rows[j][6].ToString());
                        dims.Add(h);
                        decimal longest = dims.Max();
                        dims.Remove(longest);
                        decimal medium = dims.Max();
                        dims.Remove(medium);
                        decimal shortest = dims.Min();

                        dataModel.lengthInches = longest;
                        dataModel.widthInches = medium;
                        dataModel.heightInches = shortest;
                    }
                    else
                    {
                        dataModel.heightInches = 0M;
                        dataModel.lengthInches = 0M;
                        dataModel.widthInches = 0M;
                    }
                }
                catch
                {
                    dataModel.heightInches = 0M;
                    dataModel.lengthInches = 0M;
                    dataModel.widthInches = 0M;
                }
                try
                {
                    dataModel.shipToPostalCode = dataToAnalyze.Rows[j][7].ToString();
                }
                catch
                {
                    dataModel.shipToPostalCode = "";
                }
                try
                {
                    dataModel.shipToState = dataToAnalyze.Rows[j][8].ToString().ToUpper();
                }
                catch
                {
                    dataModel.shipToState = "";
                }
                try
                {
                    if (dataToAnalyze.Rows[j][9].ToString() == null || dataToAnalyze.Rows[j][9].ToString() == "")
                    {
                        dataModel.shipToCountry = "US";

                    }
                    else
                    {
                        dataModel.shipToCountry = dataToAnalyze.Rows[j][9].ToString().ToUpper();
                    }
                }
                catch
                {
                    dataModel.shipToCountry = "US";
                }
                try
                {
                    dataModel.shipToZone = decimal.Parse(dataToAnalyze.Rows[j][10].ToString());
                }
                catch
                {
                    dataModel.shipToZone = 0M;
                }
                try
                {
                    dataModel.shippingCost = decimal.Parse(dataToAnalyze.Rows[j][11].ToString());
                }
                catch
                {
                    dataModel.shippingCost = 0M;
                }
                try
                {
                    dataModel.directReplacement = dataToAnalyze.Rows[j][12].ToString().ToUpper();
                }
                catch
                {
                    dataModel.directReplacement = "";
                }
                if (dataModel.shipToCountry != "US" && dataModel.shipToCountry != "")
                {
                    CalculateIntZone(dataModel);
                }

                dataModel.cubicCalculation = CalculateCubic(dataModel.lengthInches, dataModel.widthInches, dataModel.heightInches);

                //dataRowIndex.Add(dataModel.packageDate.ToString("d")); // what was this even for?

                //current.Add(dataModel.carrierServiceSelected); // used for the summary but can be done another way

                //AnalysisCalculations(dataModel, formData);
                //dataModel = calculations.AnalysisCalculations(dataModel, formData);

                analyzedData.Add(dataModel);

                j++;
            }

            return analyzedData;
        }

        public void CalculateIntZone(DataModel dataModel)
        {
            int row = rateTables.uspsIntZoneRowIndex.IndexOf(dataModel.shipToCountry);

            dataModel.shipToPMEIZone = decimal.Parse(rateTables.uspsIntZones.Rows[row][3].ToString());
            dataModel.shipToFCIZone = decimal.Parse(rateTables.uspsIntZones.Rows[row][5].ToString());
            if (dataModel.rates == rateTables.ps && dataModel.shipToCountry == "CA")
            {
                if (dataModel.shipToZone <= 1M)
                {
                    dataModel.shipToPMIZone = 1.4M;
                }
                else
                {
                    dataModel.shipToPMIZone = dataModel.shipToZone;
                }
            }
            else
            {
                dataModel.shipToPMIZone = decimal.Parse(rateTables.uspsIntZones.Rows[row][1].ToString());
            }
        }

        private decimal CalculateCubic(decimal length, decimal width, decimal height)
        {
            if (length == 0M || width == 0M || height == 0M)
            {
                return 0M;
            }
            else
            {
                decimal roundedLength = RoundDownNearestQuarter(length);
                decimal roundedWidth = RoundDownNearestQuarter(width);
                decimal roundedHeight = RoundDownNearestQuarter(height);

                return Math.Ceiling((roundedLength * roundedWidth * roundedHeight) / 1728M * 10M) / 10M;
            }
        }

        //private void USPSBillingWeightCalc(DataModel dataModel, FormDataModel formData)
        //{
        //    if (dataModel.cubicCalculation > 1M)
        //    {
        //        dataModel.USPSDimWeight = (decimal)Math.Ceiling((dataModel.lengthInches * dataModel.widthInches * dataModel.heightInches) / formData.uspsDim);
        //        dataModel.USPSBillingWeight = Math.Max(dataModel.USPSDimWeight, dataModel.roundedPounds);
        //    }
        //    else dataModel.USPSBillingWeight = dataModel.roundedPounds;

        //    dataModel.USPSBillingWeight = Math.Max(dataModel.roundedPounds, dataModel.USPSDimWeight);
        //}

        private decimal RoundDownNearestQuarter(decimal dec)
        {
            decimal stepOne = dec * 100m;
            int stepTwo = (int)stepOne;
            string stepThree = stepTwo.ToString();
            string stepFour = stepThree[stepThree.Length - 1].ToString();
            string stepFive = stepThree[stepThree.Length - 2].ToString();
            string stepSix = stepFour + stepFive;
            int stepSixInt = int.Parse(stepSix);
            string nearestQuarter = "";
            if (stepSixInt >= 0 && stepSixInt < 25)
            {
                nearestQuarter = "00";
            }
            else if (stepSixInt >= 25 && stepSixInt < 50)
            {
                nearestQuarter = "25";
            }
            else if (stepSixInt >= 50 && stepSixInt < 75)
            {
                nearestQuarter = "50";
            }
            else if (stepSixInt >= 75 && stepSixInt < 100)
            {
                nearestQuarter = "75";
            }
            string stepSeven = stepThree.Remove(stepThree.Length - 2);
            string stepEight = stepSeven + "." + nearestQuarter;
            decimal stepNine = decimal.Parse(stepEight);

            return stepNine;
        }
    }
}
