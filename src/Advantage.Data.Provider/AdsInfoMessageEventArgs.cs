using System;

namespace Advantage.Data.Provider
{
    public sealed class AdsInfoMessageEventArgs : EventArgs
    {
        private int miNumber;
        private string mstrMessage;

        internal AdsInfoMessageEventArgs(int iNumber, string strMessage)
        {
            this.miNumber = iNumber;
            this.mstrMessage = strMessage;
        }

        public int Number => this.miNumber;

        public string Message => this.mstrMessage;
    }
}