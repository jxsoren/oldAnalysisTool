using System;
using System.Collections.Generic;
using System.Data;

namespace AnalysisTool.Models
{
    public class RateSheetsModel
    {
        public RateSheetsModel(DataTable d, List<string> l, string r)
        {
            dataTable = d;
            rowIndex = l;
            rateSheetName = r;
        }

        public DataTable dataTable;
        public List<string> rowIndex;
        public string rateSheetName;
    }
}
