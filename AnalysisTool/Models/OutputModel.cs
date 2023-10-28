using System;

namespace AnalysisTool.Models
{
    public class OutputModel
    {
        //public DateTime packageDate;
        //public string carrierServiceSelected;
        public string currentCarrier;
        public string currentService;
        public decimal originalWeightOz;
        public decimal heightInches;
        public decimal lengthInches;
        public decimal widthInches;
        public string shipFromPostalCode;
        public string shipToPostalCode;
        public string shipToState;
        public string shipToCountry;
        public decimal shipToZone;
        public decimal shippingCost;
        public DateTime? shippingDate;
        public string? packageID;
        //ublic string directReplacement;

        public decimal roundedOunces;
        public decimal roundedPounds;
        public decimal girth;
        public decimal cubicSize;
        public decimal cubicTier;
        public decimal uspsDimWeight; // this is the weight calculation for usps using dim
        public decimal uspsBillingWeight; // this is the max between uspsdimweight and rounded pounds
        public decimal fedexDimWeight;
        public decimal fedexBillingWeight;
        public decimal upsDimWeight;
        public decimal upsBillingWeight;
        public decimal dhlDimWeight;
        public decimal dhlBillingWeight;
        public string firstClassCost;
        public string priorityMailCost;
        public string priorityMailExpressCost;
        public string priorityMailCubicCost;
        //public decimal softpackCubicCalculation;
        //public decimal softpackCubicTier;
        //public decimal softpackCubicCost;
        public string parcelSelectCost;
        public string parcelSelectCubicCost;
        //public decimal frCost;
        //public decimal freCost;
        //public decimal lfreCost;
        //public decimal pfreCost;
        //public decimal sfrbCost;
        //public decimal mfrbCost;
        //public decimal lfrbCost;
        //public decimal rrbCost;
        //public decimal apofpodpolfrgbCost;
        public decimal FedExBaseCost;
        public decimal fuelSurcharge;
        public decimal FedExTotalCost;
        //public decimal bestPrice;
        //public string bestOption;
        public string shipToFCPIZone;
        public string shipToPMIZone;
        public string shipToPMEIZone;
        public string fcpiCost;
        public string pmiCost;
        public string pmeiCost;
        //public List<RateSheetsModel> rates;
        public decimal uspsDiscount;
        public decimal uspsDim; // this is the dim value e.g. 166
        public string mipslwCost;
        public string dhleMaxCost;
        public string dhleExpCost;
        public string dhleGroundCost;
        public string dhleExpPCost;
        public string dhleGroundPCost;
        public string mipshwCost;
        public string osmpshwCost;
        public string osmpslwCost;
        public string fedexPOCost;
        public string fedexGroundCost;
        public string fedexGHCost;
        public string upsNDACost;
        public string upsNDASCost;
        public string upsSDACost;
        public string upsTDSCost;
        public string upsGroundCost;
        public string fedexSOCost;
        public string fedexTDACost;
        public string fedexTDCost;
        public string fedexESCost;
    }
}
