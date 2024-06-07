using System;
using AdvantageClientEngine;

namespace Advantage.Data.Provider
{
    public sealed class AdsBookmark
    {
        private IntPtr _mhHandle;
        private string _mstrBookmark = "";

        public AdsBookmark(IntPtr hHandle)
        {
            _mhHandle = hHandle;
            uint pulLength = 0;
            AdsException.CheckACE(ACE.AdsGetBookmarkLength(_mhHandle, ref pulLength));
            var pucBookmark = new char[pulLength];
            AdsException.CheckACE(ACE.AdsGetBookmark60(_mhHandle, pucBookmark, ref pulLength));
            _mstrBookmark = new string(pucBookmark);
        }

        internal void Goto()
        {
            AdsException.CheckACE(ACE.AdsGotoBookmark60(_mhHandle, _mstrBookmark));
        }

        internal string Bookmark => _mstrBookmark;
    }
}