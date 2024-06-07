using AdvantageClientEngine;
using System;
using System.Runtime.Serialization;

namespace Advantage.Data.Provider
{
    [Serializable]
    public sealed class AdsException : SystemException
    {
        private int mlError;
        private string mstrMessage = "";
        private string mstrState = "";

        internal static void CheckACE(uint ulRet)
        {
            if (ulRet != 0U)
                throw new AdsException(ulRet);
        }

        private void RetrieveACEError()
        {
            char[] pucBuf = new char[601];
            ushort pusBufLen = 601;
            uint pulErrCode;
            int lastError = (int)ACE.AdsGetLastError(out pulErrCode, pucBuf, ref pusBufLen);
            if (pulErrCode == 0U)
                return;
            if (pusBufLen == (ushort)0)
            {
                while ((int)pusBufLen < pucBuf.Length && pucBuf[(int)pusBufLen] != char.MinValue)
                    ++pusBufLen;
            }

            this.mstrMessage = new string(pucBuf, 0, (int)pusBufLen);
            this.mlError = (int)pulErrCode;
            if (this.mlError != 7200)
                return;
            int num1 = this.mstrMessage.IndexOf("State = ");
            if (num1 != -1)
            {
                int startIndex = num1 + "State = ".Length;
                int num2 = this.mstrMessage.IndexOf(";", startIndex);
                if (num2 != -1)
                    this.mstrState = this.mstrMessage.Substring(startIndex, num2 - startIndex);
            }

            int num3 = this.mstrMessage.IndexOf("NativeError = ");
            if (num3 == -1)
                return;
            int startIndex1 = num3 + "NativeError = ".Length;
            int num4 = this.mstrMessage.IndexOf(";", startIndex1);
            if (num4 == -1)
                return;
            string str = this.mstrMessage.Substring(startIndex1, num4 - startIndex1);
            try
            {
                int int32 = Convert.ToInt32(str);
                if (int32 == 0)
                    return;
                this.mlError = int32;
            }
            catch
            {
            }
        }

        public AdsException() => this.RetrieveACEError();

        internal AdsException(uint uiRet)
        {
            this.RetrieveACEError();
            if (this.mlError != 0)
                return;
            char[] pucBuf = new char[601];
            ushort pusBufLen = 601;
            this.mlError = (int)uiRet;
            if (ACE.AdsGetErrorString(uiRet, pucBuf, ref pusBufLen) != 0U)
                return;
            this.mstrMessage = new string(pucBuf, 0, (int)pusBufLen);
        }

        public AdsException(string strInfo)
        {
            this.RetrieveACEError();
            if (this.mstrMessage.Length > 1)
            {
                AdsException adsException = this;
                adsException.mstrMessage = adsException.mstrMessage + "  " + strInfo;
            }
            else
                this.mstrMessage = strInfo;
        }

        public AdsException(string strMessage, Exception inner)
            : base(strMessage, inner)
        {
            this.mstrMessage = strMessage;
        }

        public AdsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.mlError = info.GetInt32(nameof(Number));
            this.mstrMessage = info.GetString(nameof(Message));
            this.mstrState = info.GetString(nameof(State));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Number", this.mlError);
            info.AddValue("State", (object)this.mstrState);
            info.AddValue("Message", (object)this.mstrMessage);
        }

        public int Number => this.mlError;

        public string State => this.mstrState;

        public override string Message => this.mstrMessage;

        internal void ReplaceText(string strOrig, string strNew)
        {
            if (this.mstrMessage == null)
                return;
            this.mstrMessage = this.mstrMessage.Replace(strOrig, strNew);
        }
    }
}