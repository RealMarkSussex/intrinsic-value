using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Exceptions
{
    public class YahooIsStupidException : Exception
    {
        public YahooIsStupidException()
        {
        }

        public YahooIsStupidException(string message)
            : base(message)
        {
        }
    }
}
