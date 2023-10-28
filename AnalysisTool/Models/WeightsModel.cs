using System;
using System.Runtime.Serialization;

namespace AnalysisTool.Models
{
    [DataContract]
    public class WeightsModel
    {
        public WeightsModel(string r, int c)
        {
            this.range = r;
            this.count = c;
        }
        [DataMember(Name = "range")]
        public string range;
        [DataMember(Name = "count")]
        public int count;
    }
}
