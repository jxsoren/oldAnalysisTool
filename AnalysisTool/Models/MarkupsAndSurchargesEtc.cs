namespace AnalysisTool.Models
{
    public class MarkupsAndSurchargesEtc
    {
        //public decimal uspsDiscountPercent { get; set; }
        public decimal uspsFCDiscountPercent { get; set; }
        public decimal uspsPMDiscountPercent { get; set; }
        public decimal uspsPSDiscountPercent { get; set; }
        public decimal uspsIntDiscountPercent { get; set; }
        public decimal uspsCanadaDiscountPercent { get; set; }
        public decimal uspsDim { get; set; }
        public decimal fedexDim { get; set; }
        public decimal dhlDim { get; set; }
        public decimal fedexGeneralMarkupPercent { get; set; }
        public decimal fedexPOMarkupPercent { get; set; }
        public decimal fedexSOMarkupPercent { get; set; }
        public decimal fedexTDAMarkupPercent { get; set; }
        public decimal fedexTDMarkupPercent { get; set; }
        public decimal fedexESMarkupPercent { get; set; }
        public decimal fedexGMarkupPercent { get; set; }
        public decimal fedexGHMarkupPercent { get; set; }
        public decimal upsGeneralMarkupPercent { get; set; }
        public decimal upsNDAMarkupPercent { get; set; }
        public decimal upsNDASMarkupPercent { get; set; }
        public decimal upsSDAMarkupPercent { get; set; }
        public decimal upsTDSMarkupPercent { get; set; }
        public decimal upsGMarkupPercent { get; set; }
        public decimal osmPSLWMarkupPercent { get; set; }
        public decimal osmPSHWMarkupPercent { get; set; }
        public decimal osmFuelSurcharge { get; set; }
        public decimal miPSHWMarkupPercent { get; set; }
        public decimal miPSLW1MarkupPercent { get; set; }
        public decimal miPSLW2MarkupPercent { get; set; }
        public decimal miPSLW3MarkupPercent { get; set; }
        public decimal miPSLW4MarkupPercent { get; set; }
        public decimal miFuelSurcharge { get; set; }
        public decimal dhlGeneralMarkupPercent { get; set; }
        public decimal dhlMAXMarkupPercent { get; set; }
        public decimal dhlExpMarkupPercent { get; set; }
        public decimal dhlExpPMarkupPercent { get; set; }
        public decimal dhlGroundMarkupPercent { get; set; }
        public decimal dhlGroundPMarkupPercent { get; set; }
        public decimal dhleFuelSurcharge { get; set; }
        public bool dhleMM { get; set; }
        public bool miMM { get; set; }
        public bool osmMM { get; set; }
    }
}
