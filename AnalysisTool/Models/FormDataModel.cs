using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AnalysisTool.Models
{
    public class FormDataModel
    {
        //public bool isSoftpack { get; set; }
        //public bool strictFR { get; set; }
        //public bool includePS { get; set; }
        //public bool overallBest { get; set; }
        //public bool uspsAssessorials { get; set; }
        //public decimal uspsDiscountPercent { get; set; }
        public decimal uspsFCDiscountPercent { get; set; }
        public decimal uspsPMDiscountPercent { get; set; }
        public decimal uspsPSDiscountPercent { get; set; }
        public decimal uspsIntDiscountPercent { get; set; }
        public decimal uspsCanadaDiscountPercent { get; set; }
        public decimal uspsDim { get; set; }
        public decimal fedexDim { get; set; }
        public decimal dhlDim { get; set; }
        //public decimal residentialFee { get; set; }
        //public decimal fuelSurcharge { get; set; }
        //public string dataFile { get; set; }
        public string outputFile { get; set; }
        //public string rateSheetsLocation { get; set; }
        public string dataFilePath { get; set; }
        public IFormFile dataFile { get; set; }
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
        //public decimal upsFuelSurcharge { get;}
        public decimal osmPSHWMarkupPercent { get; set; }
        public decimal osmPSLWMarkupPercent { get; set; }
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
        public string customerId { get; set; }
        public bool includeUSPS { get; set; }
        public bool includeUPS { get; set; }
        public bool includeFedEx { get; set; }
        public bool includeDHL { get; set; }
        public bool includeOSM { get; set; }
        public bool includeMI { get; set; }
    }
}
