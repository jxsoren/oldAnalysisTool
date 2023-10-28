using AnalysisTool.Models;
using System;
using System.Collections.Generic;

namespace AnalysisTool
{
    public class Calculations
    {
        USPSRates uspsRates = new();
        decimal uspsDim;
        //public DataModel AnalysisCalculations(DataModel dataModel, FormDataModel formData)
        //{
        //    uspsDim = formData.uspsDim;

        //    try
        //    {
        //        if (dataModel.shipToCountry.ToUpper() == "US")
        //        {
        //            if (dataModel.roundedPounds > 20M || dataModel.shipToZone == 9M || dataModel.shipToState == "HI" || 
        //                dataModel.shipToState == "AK" || dataModel.shipToState == "APO/FPO/DPO")
        //            {
        //                dataModel.uspsDiscount = 1M;
        //            }
        //            else dataModel.uspsDiscount = formData.uspsDiscountPercent;

        //            dataModel = DomesticRates(dataModel);
        //        }
        //        else
        //        {
        //            if (dataModel.roundedPounds <= 20M)
        //            {
        //                if (dataModel.shipToCountry == "CA")
        //                {
        //                    dataModel.uspsDiscount = formData.uspsCanadaDiscountPercent;
        //                }
        //                else
        //                {
        //                    dataModel.uspsDiscount = formData.uspsIntDiscountPercent;
        //                }
        //            }
        //            else dataModel.uspsDiscount = 1M;

        //            dataModel = InternationalRates(dataModel);
        //        }
        //    }
        //    catch
        //    {
        //        //  ¯\_(ツ)_/¯
        //    }

        //    return dataModel;
        //}

        //public DataModel DomesticRates(DataModel dataModel)
        //{
        //    int uspsWeight; // probably don't need this
        //    int uspsWeightOz;
            
        //    dataModel.roundedPounds = (int)Math.Ceiling(dataModel.totalWeightOunces / 16);
        //    dataModel.roundedOunces = (int)Math.Ceiling(dataModel.totalWeightOunces);
        //    dataModel.girth = dataModel.lengthInches + (dataModel.widthInches * 2) + (dataModel.heightInches * 2);

        //    float cube = (float)(dataModel.lengthInches * dataModel.widthInches * dataModel.heightInches);
        //    int uspsDimWeight = (int)Math.Ceiling(((decimal)(cube) / uspsDim));
        //    if (cube > 1)
        //    {
        //        dataModel.USPSBillingWeight = Math.Max(uspsDimWeight, dataModel.roundedPounds);
        //    }
        //    else dataModel.USPSBillingWeight = dataModel.roundedPounds;

        //    try
        //    {
        //        if (dataModel.totalWeightOunces < 16)
        //        {
        //            if (dataModel.totalWeightOunces > 15)
        //            {
        //                uspsWeightOz = 16;
        //            }
        //            else
        //            {
        //                uspsWeightOz = (int)Math.Ceiling(dataModel.totalWeightOunces);
        //            }

        //            if (dataModel.lengthInches <= 22 && dataModel.widthInches <= 18 && 
        //                dataModel.heightInches <= 15 && dataModel.girth <= 108)
        //            {
        //                // usps first class rate calculation
        //                dataModel.firstClassCost = uspsRates.FirstClassCost(dataModel);
        //            }

        //            if (dataModel.lengthInches <= 24 && dataModel.girth <= 36)
        //            {
        //                // mi pslw rate calculation, possibly also osm pslw rate calculation
        //            }

        //        }

        //        if (dataModel.totalWeightOunces <= 400 && dataModel.girth <= 50 && dataModel.lengthInches <= 27 && dataModel.widthInches <= 17)
        //        {
        //            // dhle max rate calculation

        //            if (dataModel.totalWeightOunces < 16)
        //            {
        //                // dhle exp, dhle ground rate calculations
        //            }
        //            else
        //            {
        //                // dhle expp, dhle groundp rate calculations
        //            }
        //        }

        //        if (dataModel.totalWeightOunces <= 1120)
        //        {
        //            if (dataModel.girth <= 108)
        //            {
        //                // usps pm, usps pme rate calculations
        //                dataModel.priorityMailCost = uspsRates.PriorityMailCost(dataModel);
        //                dataModel.priorityMailExpressCost = uspsRates.PriorityMailExpressCost(dataModel);
        //            }

        //            if (dataModel.girth <= 130)
        //            {
        //                // usps ps rate calculations
        //                dataModel.parcelSelectCost = uspsRates.ParcelSelectCost(dataModel);
        //            }

        //            if (dataModel.girth <= 165 && dataModel.lengthInches <= 108) // need to double check OSM
        //            {
        //                // mi pshw, osm pshw rate calculations
        //            }
        //        }

        //        if (dataModel.totalWeightOunces <= 2400)
        //        {
        //            if (dataModel.girth <= 165)
        //            {
        //                if (dataModel.lengthInches <= 108)
        //                {
        //                    // fedex po, fedex ground, fedex gh, ups nda, ups ndas, ups sda, ups tda, ups ground rate calculations
        //                }

        //                if (dataModel.lengthInches <= 119)
        //                {
        //                    // fedex tda, fedex td, fedex es, fedex so
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        // log an error for that row
        //    }
        //    //psm.serviceOptions = newRates;

        //    return dataModel;
        //}

        //public DataModel InternationalRates(DataModel dataModel)
        //{

        //    return dataModel;
        //}
    }
}
