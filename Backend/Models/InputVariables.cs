using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Models
{
    public class InputVariables
    {
        public double ROIC10Y { get; set; }
        public double Revenue10Y { get; set; }
        public double FreeCashFlow10Y { get; set; }
        public double EarningsPerShare10Y { get; set; }
        public double Equity10Y { get; set; }
        public double CurrentTTMEPS { get; set; }
        public string Ticker { get; set; }
        public double GrowthEstimate { get; set; }
        public double PEHighLowAverage { get; set; }
    }
}
