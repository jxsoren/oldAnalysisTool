using System;
using System.Collections.Generic;

namespace AnalysisTool.Models
{
    public class ServiceSummary
    {
        //public ServiceSummary(string z, WeightsModel w, decimal cC, int cV, decimal pC, int pV, decimal cD)
        //{
        //    this.zones.Add(z);
        //    this.weights.Add(w);
        //    this.currCost = cC;
        //    this.currVolume = cV;
        //    this.propCost = pC;
        //    this.propVolume = pV;
        //    this.costDifference = cD;
        //}

        public List<string> zones = new List<string>();
        //public List<string> weights = new List<string>();
        public decimal currCost;
        public int currVolume;
        public decimal propCost;
        public int propVolume;
        public decimal costDifference;
        public string title;
    }
}
