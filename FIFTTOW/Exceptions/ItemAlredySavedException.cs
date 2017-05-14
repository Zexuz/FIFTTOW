using System;

namespace FIFTTOW.Exceptions
{
    public class ItemAlredySavedException : Exception
    {
        public ItemAlredySavedException(string e):base(e)
        {
        }
    }
}