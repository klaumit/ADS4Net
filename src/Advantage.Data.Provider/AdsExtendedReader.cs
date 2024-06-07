using System;
using System.Collections;
using System.Data;
using System.Globalization;
using AdvantageClientEngine;

namespace Advantage.Data.Provider
{
    public sealed class AdsExtendedReader : AdsDataReader
    {
        private IntPtr mhActiveHandle = IntPtr.Zero;
        private bool mbPartialMatch = true;
        private bool mbSettingRawKey;
        private int miProgress;
        private bool mbAttemptCancel;
        private int miNumTags = 1;
        private int miCurrentTag;
        private ACE.CallbackFn mCmdCallback;

        public event AdsInfoMessageEventHandler ProgressMessage;

        internal AdsExtendedReader(
            IntPtr hCursor,
            int iRecsAffected,
            AdsCommand adsCmd,
            AdsConnection adsConn,
            CommandBehavior eBehavior)
            : base(hCursor, iRecsAffected, adsCmd, adsConn, eBehavior)
        {
            mhActiveHandle = hCursor;
        }

        protected override void Dispose(bool bDisposing) => base.Dispose(bDisposing);

        protected override void CheckPositioned()
        {
            if ((mbBOF || mbEOF) && !mbSettingRawKey)
                throw new InvalidOperationException("There is no current record.");
        }

        public override void Close()
        {
            base.Close();
            mhActiveHandle = IntPtr.Zero;
        }

        public bool ReadPrevious()
        {
            if (mhCursor == IntPtr.Zero)
                return false;
            InvalidateRecord();
            if (!mbOpen || mbBOF)
                return false;
            if (RecordCache == 100)
                RecordCache = 10;
            AdsException.CheckACE(ACE.AdsSkip(mhActiveHandle, -1));
            ushort pbBof;
            AdsException.CheckACE(ACE.AdsAtBOF(mhCursor, out pbBof));
            if (pbBof != 0)
                mbBOF = true;
            else
                mbEOF = false;
            return !mbBOF;
        }

        public void ConvertTable(
            FilterOption filtOpt,
            string strFile,
            TableType tableType)
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsConvertTable(mhCursor, (ushort)filtOpt, strFile, (ushort)tableType));
        }

        public void CopyTable(FilterOption filtOpt, string strFile)
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsCopyTable(mhCursor, (ushort)filtOpt, strFile));
            var adsException = new AdsException();
            if (adsException.Number == 0)
                return;
            mConnection.FireWarning(adsException.Number, adsException.Message);
        }

        public void CopyTableStructure(string strFile)
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsCopyTableStructure(mhCursor, strFile));
        }

        public int LastAutoinc
        {
            get
            {
                CheckOpen();
                uint pulAutoIncVal;
                AdsException.CheckACE(ACE.AdsGetLastAutoinc(mhCursor, out pulAutoIncVal));
                return (int)pulAutoIncVal;
            }
        }

        public IntPtr AdsHandle => mhCursor;

        public IntPtr AdsActiveHandle => mhActiveHandle;

        public string EncryptionPassword
        {
            set
            {
                CheckOpen();
                ushort pbEncrypted;
                AdsException.CheckACE(ACE.AdsIsTableEncrypted(mhCursor, out pbEncrypted));
                if (pbEncrypted == 0)
                    throw new InvalidOperationException("The table is not encrypted.");
                AdsException.CheckACE(ACE.AdsEnableEncryption(mhCursor, value));
            }
        }

        public void EncryptTable(string strPassword)
        {
            AdsException.CheckACE(ACE.AdsEnableEncryption(mhCursor, strPassword));
            AdsException.CheckACE(ACE.AdsEncryptTable(mhCursor));
        }

        public void DecryptTable(string strPassword)
        {
            AdsException.CheckACE(ACE.AdsEnableEncryption(mhCursor, strPassword));
            AdsException.CheckACE(ACE.AdsDecryptTable(mhCursor));
        }

        public void CreateIndex(string strIndexName, string strExpr)
        {
            CreateIndex(null, strIndexName, strExpr, IndexOptions.Default, "");
        }

        public void CreateIndex(string strIndexName, string strExpr, string strCondition)
        {
            CreateIndex(null, strIndexName, strExpr, IndexOptions.Default, strCondition);
        }

        public void CreateIndex(
            string strIndexName,
            string strExpr,
            IndexOptions eOptions)
        {
            CreateIndex(null, strIndexName, strExpr, eOptions, "");
        }

        public void CreateIndex(
            string strFileName,
            string strIndexName,
            string strExpr,
            IndexOptions eOptions)
        {
            CreateIndex(strFileName, strIndexName, strExpr, eOptions, "");
        }

        public void CreateIndex(
            string strFileName,
            string strIndexName,
            string strExpr,
            IndexOptions eOptions,
            string strCondition)
        {
            CheckOpen();
            try
            {
                miProgress = 0;
                mbAttemptCancel = false;
                miCurrentTag = 0;
                miNumTags = 1;
                if (mCmdCallback == null)
                    mCmdCallback = ProgressCallback;
                AdsException.CheckACE(ACE.AdsRegisterCallbackFunction(mCmdCallback, 1U));
                AdsException.CheckACE(ACE.AdsCreateIndex(mhCursor, strFileName, strIndexName, strExpr,
                    strCondition, null, (ushort)eOptions, out var _));
            }
            finally
            {
                var num = (int)ACE.AdsClearCallbackFunction();
            }
        }

        private void VerifyIndexActive()
        {
            CheckOpen();
            if (mhActiveHandle == mhCursor)
                throw new InvalidOperationException("There is no active index.");
        }

        public void DeleteIndex()
        {
            VerifyIndexActive();
            AdsException.CheckACE(ACE.AdsDeleteIndex(mhActiveHandle));
            mhActiveHandle = mhCursor;
            GotoBOF();
        }

        private void OnProgressMessage(AdsInfoMessageEventArgs eventArgs)
        {
            var progressMessage = ProgressMessage;
            if (progressMessage == null)
                return;
            progressMessage(this, eventArgs);
        }

        internal void FireProgress(int iProgress)
        {
            OnProgressMessage(new AdsInfoMessageEventArgs(iProgress, "Progress"));
        }

        private uint ProgressCallback(ushort usPercentDone, uint ulCallbackID)
        {
            if (usPercentDone > 100)
                usPercentDone = 100;
            miProgress = (int)(miCurrentTag * 100.0 / miNumTags +
                                    usPercentDone / miNumTags);
            if (usPercentDone == 100)
                ++miCurrentTag;
            if (miCurrentTag == miNumTags && usPercentDone == 100)
                miProgress = 100;
            FireProgress(miProgress);
            return mbAttemptCancel ? 1U : 0U;
        }

        public void Cancel() => mbAttemptCancel = true;

        public int Progress => miProgress;

        public void OpenIndex(string strFileName)
        {
            CheckOpen();
            ushort pusArrayLen = 0;
            var ulRet = ACE.AdsOpenIndex(mhCursor, strFileName, null, ref pusArrayLen);
            switch (ulRet)
            {
                case 5005:
                    break;
                case 5065:
                    break;
                default:
                    AdsException.CheckACE(ulRet);
                    break;
            }
        }

        public object[] GetIndexNames()
        {
            CheckOpen();
            ushort pusNum;
            AdsException.CheckACE(ACE.AdsGetNumIndexes(mhCursor, out pusNum));
            var ahIndex = new IntPtr[pusNum];
            AdsException.CheckACE(ACE.AdsGetAllIndexes(mhCursor, ahIndex, ref pusNum));
            var arrayList = new ArrayList(pusNum);
            var pucName = new char[128];
            for (ushort index = 0; index < pusNum; ++index)
            {
                ushort pusLen = 128;
                AdsException.CheckACE(ACE.AdsGetIndexName(ahIndex[index], pucName, ref pusLen));
                arrayList.Add(new string(pucName, 0, pusLen));
            }

            return arrayList.ToArray();
        }

        public override bool Read() => Read(mhActiveHandle);

        public bool BOF => mbBOF;

        public bool EOF => mbEOF;

        private void CheckForBofEof()
        {
            ushort pbEof;
            AdsException.CheckACE(ACE.AdsAtEOF(mhCursor, out pbEof));
            ushort pbBof;
            AdsException.CheckACE(ACE.AdsAtBOF(mhCursor, out pbBof));
            mbEOF = pbEof != 0;
            mbBOF = pbBof != 0;
        }

        public AdsBookmark GetBookmark()
        {
            CheckOpen();
            return new AdsBookmark(mhCursor);
        }

        public void GotoBookmark(AdsBookmark bookmark)
        {
            CheckOpen();
            bookmark.Goto();
            InvalidateRecord();
            CheckForBofEof();
        }

        public int CompareBookmarks(AdsBookmark bmk1, AdsBookmark bmk2)
        {
            CheckOpen();
            var plResult = 0;
            AdsException.CheckACE(ACE.AdsCompareBookmarks(bmk1.Bookmark, bmk2.Bookmark, out plResult));
            return plResult;
        }

        public int RecordNumber
        {
            get
            {
                CheckOpen();
                if (mbBOF || mbEOF)
                    return 0;
                uint pulRec;
                AdsException.CheckACE(ACE.AdsGetRecordNum(mhCursor, 2, out pulRec));
                return (int)pulRec;
            }
            set
            {
                CheckOpen();
                AdsException.CheckACE(ACE.AdsGotoRecord(mhCursor, (uint)value));
                InvalidateRecord();
                CheckForBofEof();
            }
        }

        public int LogicalRecordNumber
        {
            get
            {
                CheckOpen();
                if (mbBOF || mbEOF)
                    return 0;
                uint logicalRecordNumber;
                AdsException.CheckACE(!(mhActiveHandle == mhCursor)
                    ? ACE.AdsGetKeyNum(mhActiveHandle, 1, out logicalRecordNumber)
                    : ACE.AdsGetRecordNum(mhActiveHandle, 1, out logicalRecordNumber));
                return (int)logicalRecordNumber;
            }
        }

        public int GetRecordCount(FilterOption eOption)
        {
            return GetRecordCount(eOption, false);
        }

        public int GetRecordCount(FilterOption eOption, bool bRefreshCount)
        {
            CheckOpen();
            var flag = true;
            if (eOption == FilterOption.RespectFilters && mhActiveHandle != mhCursor)
            {
                uint pulFlags;
                AdsException.CheckACE(ACEUNPUB.AdsGetIndexFlags(mhActiveHandle, out pulFlags));
                if ((msTableType == 3 || ((int)pulFlags & 1) != 1) && IndexCondition.Length == 0)
                {
                    ushort pusBufLen = 4082;
                    var scope = ACE.AdsGetScope(mhActiveHandle, 1, new char[pusBufLen],
                        ref pusBufLen);
                    if (scope == 5038U)
                        flag = false;
                    else
                        AdsException.CheckACE(scope);
                }
            }

            var usFilterOption = (ushort)eOption;
            if (bRefreshCount)
                usFilterOption |= 4;
            var zero = IntPtr.Zero;
            var hTable = !flag ? mhCursor : mhActiveHandle;
            uint pulCount = 0;
            AdsException.CheckACE(ACE.AdsGetRecordCount(hTable, usFilterOption, out pulCount));
            return (int)pulCount;
        }

        public void GotoBottom()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsGotoBottom(mhActiveHandle));
            InvalidateRecord();
            CheckForBofEof();
        }

        public void GotoBOF()
        {
            CheckOpen();
            SetBOF();
            InvalidateRecord();
        }

        public void GotoTop()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsGotoTop(mhActiveHandle));
            InvalidateRecord();
            CheckForBofEof();
        }

        public string ActiveIndex
        {
            get
            {
                VerifyIndexActive();
                ushort pusLen = 128;
                var pucName = new char[pusLen];
                AdsException.CheckACE(ACE.AdsGetIndexName(mhActiveHandle, pucName, ref pusLen));
                return new string(pucName, 0, pusLen);
            }
            set
            {
                if (value == null || value == "")
                {
                    mhActiveHandle = mhCursor;
                }
                else
                {
                    CheckOpen();
                    IntPtr phIndex;
                    AdsException.CheckACE(ACE.AdsGetIndexHandle(mhCursor, value, out phIndex));
                    mhActiveHandle = phIndex;
                }

                GotoBOF();
            }
        }

        public double RelativeKeyPosition
        {
            get
            {
                VerifyIndexActive();
                double pdPos;
                AdsException.CheckACE(ACE.AdsGetRelKeyPos(mhActiveHandle, out pdPos));
                return pdPos;
            }
            set
            {
                CheckOpen();
                if (mhActiveHandle == mhCursor)
                    throw new InvalidOperationException("Invalid handle.");
                AdsException.CheckACE(ACE.AdsSetRelKeyPos(mhActiveHandle, value));
                InvalidateRecord();
                CheckForBofEof();
            }
        }

        private byte[] BuildRawKey(object[] keyValues, out ushort usKeyLen)
        {
            CheckOpen();
            ushort length;
            AdsException.CheckACE(ACEUNPUB.AdsGetNumSegments(mhActiveHandle, out length));
            if (keyValues.Length > length)
                throw new ArgumentException("Too many objects in argument array.");
            AdsException.CheckACE(ACE.AdsInitRawKey(mhActiveHandle));
            var pusSegFieldNumbers = new ushort[length];
            AdsException.CheckACE(
                ACEUNPUB.AdsGetSegmentFieldNumbers(mhActiveHandle, out length, pusSegFieldNumbers));
            try
            {
                mbSettingRawKey = true;
                for (ushort index = 0; index < keyValues.Length; ++index)
                {
                    if (keyValues.Length > index)
                    {
                        if (keyValues[index] == null || keyValues[index] == DBNull.Value)
                            AdsException.CheckACE(ACE.AdsSetEmpty(mhActiveHandle,
                                pusSegFieldNumbers[index]));
                        else if (Type.GetTypeCode(keyValues[index].GetType()) == TypeCode.String)
                        {
                            var pwcBuf = (string)keyValues[index];
                            int num;
                            if (PartialMatch)
                            {
                                num = pwcBuf.Length;
                            }
                            else
                            {
                                num = GetACEFieldLength(pusSegFieldNumbers[index] - 1);
                                pwcBuf = pwcBuf.PadRight(num);
                            }

                            AdsException.CheckACE(ACE.AdsSetStringW(mhActiveHandle,
                                pusSegFieldNumbers[index], pwcBuf, (uint)num));
                        }
                        else
                            SetValue(pusSegFieldNumbers[index] - 1, keyValues[index]);
                    }
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mbSettingRawKey = false;
            }

            usKeyLen = 0;
            AdsException.CheckACE(ACE.AdsGetKeyLength(mhActiveHandle, out usKeyLen));
            var pucKey = new byte[usKeyLen];
            AdsException.CheckACE(ACE.AdsBuildRawKey(mhActiveHandle, pucKey, ref usKeyLen));
            return pucKey;
        }

        public bool PartialMatch
        {
            get => mbPartialMatch;
            set => mbPartialMatch = value;
        }

        public void SetRange(object[] topScope, object[] bottomScope)
        {
            VerifyIndexActive();
            if (topScope != null && topScope.Length > 0)
            {
                ushort usKeyLen;
                AdsException.CheckACE(ACE.AdsSetScope(mhActiveHandle, 1,
                    BuildRawKey(topScope, out usKeyLen), usKeyLen, 1));
            }
            else
            {
                var ulRet = ACE.AdsClearScope(mhActiveHandle, 1);
                if (ulRet != 5038U)
                    AdsException.CheckACE(ulRet);
            }

            if (bottomScope != null && bottomScope.Length > 0)
            {
                ushort usKeyLen;
                AdsException.CheckACE(ACE.AdsSetScope(mhActiveHandle, 2,
                    BuildRawKey(bottomScope, out usKeyLen), usKeyLen, 1));
            }
            else
            {
                var ulRet = ACE.AdsClearScope(mhActiveHandle, 2);
                if (ulRet != 5038U)
                    AdsException.CheckACE(ulRet);
            }

            GotoBOF();
        }

        public void ClearRange()
        {
            CheckOpen();
            var ulRet1 = ACE.AdsClearScope(mhActiveHandle, 1);
            if (ulRet1 != 5038U)
                AdsException.CheckACE(ulRet1);
            var ulRet2 = ACE.AdsClearScope(mhActiveHandle, 2);
            if (ulRet2 != 5038U)
                AdsException.CheckACE(ulRet2);
            GotoBOF();
        }

        public bool Seek(object[] seekValue, SeekType seekType)
        {
            VerifyIndexActive();
            if (seekValue == null || seekValue.Length == 0)
                throw new ArgumentException("Seek called with no seek value.");
            ushort pbFound = 0;
            switch (seekType)
            {
                case SeekType.SoftSeek:
                case SeekType.HardSeek:
                case SeekType.SeekGT:
                    ushort usKeyLen1;
                    AdsException.CheckACE(ACE.AdsSeek(mhActiveHandle, BuildRawKey(seekValue, out usKeyLen1),
                        usKeyLen1, 1, (ushort)seekType, out pbFound));
                    break;
                case SeekType.SeekLast:
                    ushort usKeyLen2;
                    AdsException.CheckACE(ACE.AdsSeekLast(mhActiveHandle,
                        BuildRawKey(seekValue, out usKeyLen2), usKeyLen2, 1, out pbFound));
                    break;
                default:
                    throw new NotSupportedException("Unexpected SeekType.");
            }

            InvalidateRecord();
            CheckForBofEof();
            return pbFound != 0;
        }

        public bool IsIndexUnique
        {
            get
            {
                VerifyIndexActive();
                ushort pbUnique;
                var num = (int)ACE.AdsIsIndexUnique(mhActiveHandle, out pbUnique);
                return pbUnique != 0;
            }
        }

        public bool IsIndexCandidate
        {
            get
            {
                VerifyIndexActive();
                ushort pbCandidate;
                var num = (int)ACE.AdsIsIndexCandidate(mhActiveHandle, out pbCandidate);
                return pbCandidate != 0;
            }
        }

        public bool IsIndexCompound
        {
            get
            {
                VerifyIndexActive();
                ushort pbCompound;
                var num = (int)ACE.AdsIsIndexCompound(mhActiveHandle, out pbCompound);
                return pbCompound != 0;
            }
        }

        public bool IsIndexCustom
        {
            get
            {
                VerifyIndexActive();
                ushort pbCustom;
                var num = (int)ACE.AdsIsIndexCustom(mhActiveHandle, out pbCustom);
                return pbCustom != 0;
            }
        }

        public bool IsIndexDescending
        {
            get
            {
                VerifyIndexActive();
                ushort pbDescending;
                var num = (int)ACE.AdsIsIndexDescending(mhActiveHandle, out pbDescending);
                return pbDescending != 0;
            }
        }

        public bool IsIndexPrimaryKey
        {
            get
            {
                VerifyIndexActive();
                ushort pbPrimaryKey;
                var num = (int)ACE.AdsIsIndexPrimaryKey(mhActiveHandle, out pbPrimaryKey);
                return pbPrimaryKey != 0;
            }
        }

        public string IndexExpression
        {
            get
            {
                VerifyIndexActive();
                ushort pusLen = 510;
                var pucExpr = new char[pusLen];
                var indexExpr = ACE.AdsGetIndexExpr(mhActiveHandle, pucExpr, ref pusLen);
                if (indexExpr == 5005U)
                {
                    pucExpr = new char[pusLen];
                    indexExpr = ACE.AdsGetIndexExpr(mhActiveHandle, pucExpr, ref pusLen);
                }

                AdsException.CheckACE(indexExpr);
                return new string(pucExpr, 0, pusLen);
            }
        }

        public string IndexCondition
        {
            get
            {
                VerifyIndexActive();
                ushort pusLen = 510;
                var pucExpr = new char[pusLen];
                var indexCondition = ACE.AdsGetIndexCondition(mhActiveHandle, pucExpr, ref pusLen);
                if (indexCondition == 5005U)
                {
                    pucExpr = new char[pusLen];
                    indexCondition = ACE.AdsGetIndexCondition(mhActiveHandle, pucExpr, ref pusLen);
                }

                AdsException.CheckACE(indexCondition);
                return new string(pucExpr, 0, pusLen);
            }
        }

        public string Filter
        {
            get
            {
                uint pulLen = 0;
                var aoF100 = ACE.AdsGetAOF100(mhCursor, 8192U, null, ref pulLen);
                switch (aoF100)
                {
                    case 5005:
                        var pvFilter = new char[pulLen];
                        AdsException.CheckACE(ACE.AdsGetAOF100(mhCursor, 8192U, pvFilter, ref pulLen));
                        return new string(pvFilter);
                    case 5037:
                        return new string(' ', 0);
                    default:
                        AdsException.CheckACE(aoF100);
                        return new string(' ', 0);
                }
            }
            set
            {
                CheckOpen();
                if (value == null || value.Length == 0)
                {
                    AdsException.CheckACE(ACE.AdsClearAOF(mhCursor));
                }
                else
                {
                    AdsException.CheckACE(ACE.AdsSetExact22(mhCursor,
                        mbPartialMatch ? (ushort)0 : (ushort)1));
                    AdsException.CheckACE(ACE.AdsSetAOF100(mhCursor, value, 8194U));
                }

                GotoBOF();
            }
        }

        public void LockRecord() => LockRecord(0);

        public void LockRecord(int iRecordNumber)
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsLockRecord(mhCursor, (uint)iRecordNumber));
        }

        public void UnlockRecord() => UnlockRecord(0);

        public void UnlockRecord(int iRecordNumber)
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsUnlockRecord(mhCursor, (uint)iRecordNumber));
        }

        public bool IsRecordLocked() => IsRecordLocked(0);

        public bool IsRecordLocked(int iRecordNumber)
        {
            CheckOpen();
            ushort pbLocked;
            AdsException.CheckACE(ACE.AdsIsRecordLocked(mhCursor, (uint)iRecordNumber, out pbLocked));
            return pbLocked != 0;
        }

        public void LockTable()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsLockTable(mhCursor));
        }

        public void UnlockTable()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsUnlockTable(mhCursor));
        }

        public bool IsTableLocked()
        {
            CheckOpen();
            ushort pbLocked;
            AdsException.CheckACE(ACE.AdsIsTableLocked(mhCursor, out pbLocked));
            return pbLocked != 0;
        }

        public void PackTable()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsPackTable(mhCursor));
            InvalidateRecord();
            CheckForBofEof();
        }

        public void ZapTable()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsZapTable(mhCursor));
            InvalidateRecord();
            CheckForBofEof();
        }

        public void Reindex() => Reindex(0);

        public void Reindex(int iPageSize)
        {
            CheckOpen();
            if (iPageSize < 0)
                throw new ArgumentException("Page Size cannot be negative.");
            try
            {
                miProgress = 0;
                mbAttemptCancel = false;
                miCurrentTag = 0;
                ushort pusNum;
                AdsException.CheckACE(ACE.AdsGetNumIndexes(mhCursor, out pusNum));
                miNumTags = pusNum;
                AdsException.CheckACE(ACE.AdsGetNumFTSIndexes(mhCursor, out pusNum));
                miNumTags += pusNum;
                if (mCmdCallback == null)
                    mCmdCallback = ProgressCallback;
                AdsException.CheckACE(ACE.AdsRegisterCallbackFunction(mCmdCallback, 1U));
                AdsException.CheckACE(ACE.AdsReindex61(mhCursor, (uint)iPageSize));
                InvalidateRecord();
                CheckForBofEof();
            }
            finally
            {
                var num = (int)ACE.AdsClearCallbackFunction();
            }
        }

        public void RecallAllRecords()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsRecallAllRecords(mhActiveHandle));
            InvalidateRecord();
            CheckForBofEof();
        }

        private void CheckForSet(int iCol)
        {
            CheckOpen();
            CheckPositioned();
            CheckColumnIndex(iCol);
        }

        public new object this[int i]
        {
            get => GetValue(i);
            set => SetValue(i, value);
        }

        public new object this[string strName]
        {
            get => this[GetOrdinal(strName)];
            set => SetValue(GetOrdinal(strName), value);
        }

        private void SetFieldNullOrEmpty(IntPtr hHandle, int iCol)
        {
            var ulRet = ACE.AdsSetNull(hHandle, (uint)(iCol + 1));
            if (ulRet == 5205U)
                ulRet = ACE.AdsSetEmpty(hHandle, (uint)(iCol + 1));
            AdsException.CheckACE(ulRet);
        }

        public void SetValue(int iCol, object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Empty:
                case TypeCode.DBNull:
                    CheckForSet(iCol);
                    SetFieldNullOrEmpty(mbSettingRawKey ? mhActiveHandle : mhCursor, iCol);
                    break;
                case TypeCode.Object:
                    var type = value.GetType();
                    if (type == (object)Type.GetType("System.Byte[]"))
                    {
                        SetBytes(iCol, (byte[])value);
                        break;
                    }

                    if (type == (object)Type.GetType("System.Char[]"))
                    {
                        SetChars(iCol, (char[])value);
                        break;
                    }

                    if (type != (object)Type.GetType("System.TimeSpan"))
                        throw new SystemException("Invalid data type.");
                    SetTimeSpan(iCol, (TimeSpan)value);
                    break;
                case TypeCode.Boolean:
                    SetBoolean(iCol, (bool)value);
                    break;
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                    SetByte(iCol, (byte)value);
                    break;
                case TypeCode.Int16:
                    SetInt16(iCol, (short)value);
                    break;
                case TypeCode.UInt16:
                case TypeCode.Int32:
                    SetInt32(iCol, (int)value);
                    break;
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    SetInt64(iCol, (long)value);
                    break;
                case TypeCode.Single:
                    SetFloat(iCol, (float)value);
                    break;
                case TypeCode.Double:
                    SetDouble(iCol, (double)value);
                    break;
                case TypeCode.Decimal:
                    SetDecimal(iCol, Convert.ToDecimal(value));
                    break;
                case TypeCode.DateTime:
                    SetDateTime(iCol, (DateTime)value);
                    break;
                case TypeCode.String:
                    SetString(iCol, (string)value);
                    break;
                default:
                    throw new NotSupportedException("Unexpected object type.");
            }
        }

        public void SetBoolean(int iCol, bool value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetLogical(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value ? (ushort)1 : (ushort)0));
            InvalidateRecord();
        }

        public void SetByte(int iCol, byte value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetShort(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value));
            InvalidateRecord();
        }

        public void SetBytes(int iCol, byte[] value)
        {
            var length = value.Length;
            SetBytes(iCol, 0, length, value, 0, length);
        }

        public void SetBytes(
            int iCol,
            int iFieldOffset,
            int iTotalLength,
            byte[] buffer,
            int iBufferOffset,
            int iLength)
        {
            CheckForSet(iCol);
            var num = mbSettingRawKey ? mhActiveHandle : mhCursor;
            if (iLength == 0)
            {
                SetFieldNullOrEmpty(num, iCol);
            }
            else
            {
                if (iBufferOffset >= buffer.Length)
                    throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                if (iLength + iBufferOffset > buffer.Length)
                    throw new IndexOutOfRangeException(
                        "The buffer offset plus the amount to write exceeds the array size.");
                var aceFieldType = (ushort)GetACEFieldType(iCol);
                switch (aceFieldType)
                {
                    case 6:
                    case 7:
                        if (iBufferOffset == 0)
                        {
                            AdsException.CheckACE(ACE.AdsSetBinary(num, (uint)(iCol + 1), aceFieldType,
                                (uint)iTotalLength, (uint)iFieldOffset, buffer, (uint)iLength));
                            break;
                        }

                        var numArray = new byte[iLength];
                        Array.Copy(buffer, iBufferOffset, numArray, 0, iLength);
                        AdsException.CheckACE(ACE.AdsSetBinary(num, (uint)(iCol + 1), 6, (uint)iTotalLength,
                            (uint)iFieldOffset, numArray, (uint)iLength));
                        break;
                    case 16:
                    case 24:
                        if (iFieldOffset != 0)
                            throw new ArgumentException("Raw field type does not accept a field offset.");
                        if (iBufferOffset == 0)
                        {
                            AdsException.CheckACE(ACE.AdsSetField(num, (uint)(iCol + 1), buffer, (uint)iLength));
                            break;
                        }

                        var destinationArray = new byte[iLength];
                        Array.Copy(buffer, iBufferOffset, destinationArray, 0, iLength);
                        AdsException.CheckACE(ACE.AdsSetField(num, (uint)(iCol + 1), buffer, (uint)iLength));
                        break;
                    default:
                        throw new InvalidCastException("Invalid field type.");
                }
            }

            InvalidateRecord();
        }

        public void SetChars(int iCol, char[] value)
        {
            CheckForSet(iCol);
            var hObj = mbSettingRawKey ? mhActiveHandle : mhCursor;
            var pwcBuf = new string(value);
            AdsException.CheckACE(ACE.AdsSetStringW(hObj, (uint)(iCol + 1), pwcBuf, (uint)pwcBuf.Length));
            InvalidateRecord();
        }

        public void SetInt16(int iCol, short value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetLong(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value));
            InvalidateRecord();
        }

        public void SetInt32(int iCol, int value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetDouble(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value));
            InvalidateRecord();
        }

        public void SetInt64(int iCol, long value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetLongLong(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value));
            InvalidateRecord();
        }

        public void SetFloat(int iCol, float value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetDouble(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value));
            InvalidateRecord();
        }

        public void SetDouble(int iCol, double value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetDouble(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value));
            InvalidateRecord();
        }

        public void SetString(int iCol, string value)
        {
            CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetStringW(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), value, (uint)value.Length));
            InvalidateRecord();
        }

        public void SetDecimal(int iCol, Decimal value)
        {
            CheckForSet(iCol);
            var aceFieldType = (ushort)GetACEFieldType(iCol);
            var hObj = mbSettingRawKey ? mhActiveHandle : mhCursor;
            if (aceFieldType == 18)
            {
                var oaCurrency = Decimal.ToOACurrency(value);
                AdsException.CheckACE(ACE.AdsSetMoney(hObj, (uint)(iCol + 1), oaCurrency));
            }
            else
            {
                var pwcBuf = value.ToString(CultureInfo.InvariantCulture.NumberFormat);
                AdsException.CheckACE(ACE.AdsSetFieldW(hObj, (uint)(iCol + 1), pwcBuf, (uint)pwcBuf.Length));
            }

            InvalidateRecord();
        }

        public void SetDateTime(int iCol, DateTime value)
        {
            CheckForSet(iCol);
            var pucJulian = value.ToString("yyyyMMdd");
            double pdJulian;
            AdsException.CheckACE(ACEUNPUB.AdsConvertStringToJulian(pucJulian, (ushort)pucJulian.Length, out pdJulian));
            var hObj = mbSettingRawKey ? mhActiveHandle : mhCursor;
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 3:
                    AdsException.CheckACE(ACE.AdsSetJulian(hObj, (uint)(iCol + 1), (int)pdJulian));
                    break;
                case 14:
                case 22:
                    var num1 = ((value.Hour * 60 + value.Minute) * 60 + value.Second) * 1000 + value.Millisecond;
                    var num2 = (ulong)pdJulian;
                    var num3 = (ulong)num1;
                    var pucBuf = !BitConverter.IsLittleEndian ? num2 << 32 | num3 : num3 << 32 | num2;
                    AdsException.CheckACE(ACEUNPUB.AdsSetTimeStampRaw(hObj, (uint)(iCol + 1), ref pucBuf, 8U));
                    break;
                default:
                    throw new NotSupportedException("Invalid field type.");
            }

            InvalidateRecord();
        }

        public void SetTimeSpan(int iCol, TimeSpan value)
        {
            CheckForSet(iCol);
            var lTime = value.Days == 0
                ? (int)value.TotalMilliseconds
                : throw new ArgumentException("TimeSpan exceeds 1 day. Cannot set time field.");
            if (lTime < 0)
                throw new ArgumentException("TimeSpan is negative. Cannot set time field.");
            AdsException.CheckACE(ACE.AdsSetMilliseconds(mbSettingRawKey ? mhActiveHandle : mhCursor,
                (uint)(iCol + 1), lTime));
            InvalidateRecord();
        }

        public void AppendRecord()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsAppendRecord(mhCursor));
            mbBOF = mbEOF = false;
        }

        public void DeleteRecord()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsDeleteRecord(mhCursor));
        }

        public bool IsRecordDeleted()
        {
            CheckOpen();
            ushort pbDeleted;
            AdsException.CheckACE(ACE.AdsIsRecordDeleted(mhCursor, out pbDeleted));
            return pbDeleted != 0;
        }

        public void RecallRecord()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsRecallRecord(mhCursor));
        }

        public void WriteRecord()
        {
            CheckOpen();
            AdsException.CheckACE(ACE.AdsWriteRecord(mhCursor));
            InvalidateRecord();
        }

        [Flags]
        public enum IndexOptions
        {
            Default = 0,
            Unique = 1,
            Compound = 2,
            Descending = 8,
            Candidate = 2048, // 0x00000800
            Binary = 4096, // 0x00001000
        }

        public enum FilterOption
        {
            RespectFilters = 1,
            IgnoreFilters = 2,
            RespectScopes = 3,
        }

        public enum TableType
        {
            NTX = 1,
            CDX = 2,
            ADT = 3,
            VFP = 4,
        }

        public enum SeekType
        {
            SoftSeek = 1,
            HardSeek = 2,
            SeekLast = 3,
            SeekGT = 4,
        }
    }
}