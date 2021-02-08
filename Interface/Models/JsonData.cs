using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface.Models
{
    public class JsonData
    {
        public List<OutputVariables> OutputNumbers { get; set; }
        public string Date { get; set; }
        public List<string> Tickers { get; set; }
    }
}
