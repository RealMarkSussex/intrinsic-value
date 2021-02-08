using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Models
{
    public class OutputVariables
    {
        public double ROIC10Y { get; set; }
        public double Revenue10Y { get; set; }
        public double FreeCashFlow10Y { get; set; }
        public double EarningsPerShare10Y { get; set; }
        public double Equity10Y { get; set; }
        public double CurrentTTMEps { get; set; }
        public double EstimatedEps { get; set; }
        public double EstimatePE { get; set; }
        public double StickerPrice { get; set; }
        public string Ticker { get; set; }

    }
}
