using System;
using System.Runtime.Serialization;
using AdvantageClientEngine;

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
            var pucBuf = new char[601];
            ushort pusBufLen = 601;
            uint pulErrCode;
            var lastError = (int)ACE.AdsGetLastError(out pulErrCode, pucBuf, ref pusBufLen);
            if (pulErrCode == 0U)
                return;
            if (pusBufLen == 0)
            {
                while (pusBufLen < pucBuf.Length && pucBuf[pusBufLen] != char.MinValue)
                    ++pusBufLen;
            }

            mstrMessage = new string(pucBuf, 0, pusBufLen);
            mlError = (int)pulErrCode;
            if (mlError != 7200)
                return;
            var num1 = mstrMessage.IndexOf("State = ");
            if (num1 != -1)
            {
                var startIndex = num1 + "State = ".Length;
                var num2 = mstrMessage.IndexOf(";", startIndex);
                if (num2 != -1)
                    mstrState = mstrMessage.Substring(startIndex, num2 - startIndex);
            }

            var num3 = mstrMessage.IndexOf("NativeError = ");
            if (num3 == -1)
                return;
            var startIndex1 = num3 + "NativeError = ".Length;
            var num4 = mstrMessage.IndexOf(";", startIndex1);
            if (num4 == -1)
                return;
            var str = mstrMessage.Substring(startIndex1, num4 - startIndex1);
            try
            {
                var int32 = Convert.ToInt32(str);
                if (int32 == 0)
                    return;
                mlError = int32;
            }
            catch
            {
            }
        }

        public AdsException() => RetrieveACEError();

        internal AdsException(uint uiRet)
        {
            RetrieveACEError();
            if (mlError != 0)
                return;
            var pucBuf = new char[601];
            ushort pusBufLen = 601;
            mlError = (int)uiRet;
            if (ACE.AdsGetErrorString(uiRet, pucBuf, ref pusBufLen) != 0U)
                return;
            mstrMessage = new string(pucBuf, 0, pusBufLen);
        }

        public AdsException(string strInfo)
        {
            RetrieveACEError();
            if (mstrMessage.Length > 1)
            {
                var adsException = this;
                adsException.mstrMessage = adsException.mstrMessage + "  " + strInfo;
            }
            else
                mstrMessage = strInfo;
        }

        public AdsException(string strMessage, Exception inner)
            : base(strMessage, inner)
        {
            mstrMessage = strMessage;
        }

        public AdsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            mlError = info.GetInt32(nameof(Number));
            mstrMessage = info.GetString(nameof(Message));
            mstrState = info.GetString(nameof(State));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Number", mlError);
            info.AddValue("State", mstrState);
            info.AddValue("Message", mstrMessage);
        }

        public int Number => mlError;

        public string State => mstrState;

        public override string Message => mstrMessage;

        internal void ReplaceText(string strOrig, string strNew)
        {
            if (mstrMessage == null)
                return;
            mstrMessage = mstrMessage.Replace(strOrig, strNew);
        }
    }
}