using System;
using System.Collections.Generic;

namespace AnalysisTool.Models
{
    public class DataModel
    {
        public DateTime packageDate;
        public string carrierServiceSelected;
        public string currentCarrier;
        public string currentService;
        public decimal totalWeightOunces;
        public decimal heightInches;
        public decimal lengthInches;
        public decimal widthInches;
        public string shipFromPostalCode;
        public string shipToPostalCode;
        public string shipToState;
        public string shipToCountry;
        public decimal shipToZone;
        public decimal shippingCost;
        public string directReplacement;

        public decimal roundedOunces;
        public decimal roundedPounds;
        public decimal girth;
        public decimal cubicCalculation;
        public decimal USPSDimWeight;
        public decimal USPSBillingWeight;
        public decimal FedExDimWeight;
        public decimal FedExBillingWeight;
        public decimal firstClassCost;
        public decimal priorityMailCost;
        public decimal priorityMailExpressCost;
        public decimal cubicCost;
        public decimal softpackCubicCalculation;
        public decimal softpackCubicTier;
        public decimal softpackCubicCost;
        public decimal parcelSelectCost;
        public decimal frCost;
        public decimal freCost;
        public decimal lfreCost;
        public decimal pfreCost;
        public decimal sfrbCost;
        public decimal mfrbCost;
        public decimal lfrbCost;
        public decimal rrbCost;
        public decimal apofpodpolfrgbCost;
        public decimal FedExBaseCost;
        public decimal fuelSurcharge;
        public decimal FedExTotalCost;
        public decimal bestPrice;
        public string bestOption;
        public decimal shipToFCIZone;
        public decimal shipToPMIZone;
        public decimal shipToPMEIZone;
        public decimal fciCost;
        public decimal pmiCost;
        public decimal pmeiCost;
        public List<RateSheetsModel> rates;
        public decimal uspsDiscount;
        public decimal uspsDim;
    }
}