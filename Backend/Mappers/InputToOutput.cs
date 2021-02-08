using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Mappers
{
    public class InputToOutput
    {
        // TODO Get average historical PE and use lowest of estimate PE and historical average PE in calcualtions
        public OutputVariables inputToOutput(InputVariables inputVariables)
        {
            var estimatedEps = Math.Min(inputVariables.Equity10Y, inputVariables.GrowthEstimate);
            double timeToDoubleEps;
            if (estimatedEps == -1)
            {
                timeToDoubleEps = -1;
            } 
            else
            {
                timeToDoubleEps = Math.Log(2) / Math.Log((estimatedEps / 100) + 1);
            }
            var epsIn10Years = (10 / timeToDoubleEps) * inputVariables.CurrentTTMEPS;
            var estimatePe = Math.Min(estimatedEps * 2, inputVariables.PEHighLowAverage);
            var futureMarketPrice = estimatePe * epsIn10Years;
            var stickerPrice = futureMarketPrice / 4;

            return new OutputVariables()
            {
                ROIC10Y = inputVariables.ROIC10Y,
                Revenue10Y = inputVariables.Revenue10Y,
                FreeCashFlow10Y = inputVariables.FreeCashFlow10Y,
                EarningsPerShare10Y = inputVariables.EarningsPerShare10Y,
                Equity10Y = inputVariables.Equity10Y,
                CurrentTTMEps = inputVariables.CurrentTTMEPS,
                Ticker = inputVariables.Ticker,
                GrowthEstimate = inputVariables.GrowthEstimate,
                PEHighLowAverage = inputVariables.PEHighLowAverage,
                EstimatedEps = estimatedEps,
                EstimatePE = estimatePe,
                StickerPrice = stickerPrice,
            };
        }
    }
}
