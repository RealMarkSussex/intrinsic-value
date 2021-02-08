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
            var estimatedEps = inputVariables.Equity10Y;
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
            var estimatePe = estimatedEps * 2;
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
                EstimatedEps = estimatedEps,
                EstimatePE = estimatePe,
                StickerPrice = stickerPrice,
            };
        }
    }
}
