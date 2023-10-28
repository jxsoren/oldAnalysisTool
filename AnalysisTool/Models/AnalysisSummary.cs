using System;
using System.Collections.Generic;

namespace AnalysisTool.Models
{
    public class AnalysisSummary
    {
        public string service { get; set; }
        public int packages { get; set; }
        public decimal cost { get; set; }
        public List<string> state { get; set; }
        public List<string> country { get; set; }
        public List<string> zone { get; set; }
    }
}
