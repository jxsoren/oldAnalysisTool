using System;
using System.Runtime.Serialization;

namespace AnalysisTool.Models
{
    [DataContract]
    public class Summary
    {
        public Summary(string x, int y, string z, decimal c, decimal p)
        {
            this.id = x;
            this.value = y;
            this.title = z;
            this.currentSpend = c;
            this.proposedSpend = p;
        }
        [DataMember(Name = "id")]
        public string id;
        [DataMember(Name = "value")]
        public int value;
        [DataMember(Name = "title")]
        public string title;
        [DataMember(Name = "current spend")]
        public decimal currentSpend;
        [DataMember(Name = "proposed spend")]
        public decimal proposedSpend;
    }
}
