using System;

namespace FIFTTOW.Exceptions
{
    public class WifiNetworkNotFoundException : Exception
    {
        public WifiNetworkNotFoundException(string s):base(s)
        {
        }
    }
}