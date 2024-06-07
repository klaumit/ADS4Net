using System;

namespace Advantage.Data.Provider
{
    public sealed class AdsInfoMessageEventArgs : EventArgs
    {
        private int miNumber;
        private string mstrMessage;

        internal AdsInfoMessageEventArgs(int iNumber, string strMessage)
        {
            miNumber = iNumber;
            mstrMessage = strMessage;
        }

        public int Number => miNumber;

        public string Message => mstrMessage;
    }
}