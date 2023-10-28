using System;
using CsvHelper.Configuration.Attributes;

namespace AnalysisTool.Models
{
    public class InputModel
    {
        public string? carrier { get; set; }
        public string? service { get; set; }
        public string? cost { get; set; }
        public DateTime? date { get; set; }
        public float? weight { get; set; }
        public float? length { get; set; }
        public float? width { get; set; }
        public float? height { get; set; }
        public string? toPostalCode { get; set; }
        public string? fromPostalCode { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public float? zone { get; set; }
        public string? packageID { get; set; }
    }
}
