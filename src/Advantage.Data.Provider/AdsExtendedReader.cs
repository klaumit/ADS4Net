using AdvantageClientEngine;
using System;
using System.Collections;
using System.Data;
using System.Globalization;

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
            this.mhActiveHandle = hCursor;
        }

        protected override void Dispose(bool bDisposing) => base.Dispose(bDisposing);

        protected override void CheckPositioned()
        {
            if ((this.mbBOF || this.mbEOF) && !this.mbSettingRawKey)
                throw new InvalidOperationException("There is no current record.");
        }

        public override void Close()
        {
            base.Close();
            this.mhActiveHandle = IntPtr.Zero;
        }

        public bool ReadPrevious()
        {
            if (this.mhCursor == IntPtr.Zero)
                return false;
            this.InvalidateRecord();
            if (!this.mbOpen || this.mbBOF)
                return false;
            if (this.RecordCache == (short)100)
                this.RecordCache = (short)10;
            AdsException.CheckACE(ACE.AdsSkip(this.mhActiveHandle, -1));
            ushort pbBof;
            AdsException.CheckACE(ACE.AdsAtBOF(this.mhCursor, out pbBof));
            if (pbBof != (ushort)0)
                this.mbBOF = true;
            else
                this.mbEOF = false;
            return !this.mbBOF;
        }

        public void ConvertTable(
            AdsExtendedReader.FilterOption filtOpt,
            string strFile,
            AdsExtendedReader.TableType tableType)
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsConvertTable(this.mhCursor, (ushort)filtOpt, strFile, (ushort)tableType));
        }

        public void CopyTable(AdsExtendedReader.FilterOption filtOpt, string strFile)
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsCopyTable(this.mhCursor, (ushort)filtOpt, strFile));
            AdsException adsException = new AdsException();
            if (adsException.Number == 0)
                return;
            this.mConnection.FireWarning(adsException.Number, adsException.Message);
        }

        public void CopyTableStructure(string strFile)
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsCopyTableStructure(this.mhCursor, strFile));
        }

        public int LastAutoinc
        {
            get
            {
                this.CheckOpen();
                uint pulAutoIncVal;
                AdsException.CheckACE(ACE.AdsGetLastAutoinc(this.mhCursor, out pulAutoIncVal));
                return (int)pulAutoIncVal;
            }
        }

        public IntPtr AdsHandle => this.mhCursor;

        public IntPtr AdsActiveHandle => this.mhActiveHandle;

        public string EncryptionPassword
        {
            set
            {
                this.CheckOpen();
                ushort pbEncrypted;
                AdsException.CheckACE(ACE.AdsIsTableEncrypted(this.mhCursor, out pbEncrypted));
                if (pbEncrypted == (ushort)0)
                    throw new InvalidOperationException("The table is not encrypted.");
                AdsException.CheckACE(ACE.AdsEnableEncryption(this.mhCursor, value));
            }
        }

        public void EncryptTable(string strPassword)
        {
            AdsException.CheckACE(ACE.AdsEnableEncryption(this.mhCursor, strPassword));
            AdsException.CheckACE(ACE.AdsEncryptTable(this.mhCursor));
        }

        public void DecryptTable(string strPassword)
        {
            AdsException.CheckACE(ACE.AdsEnableEncryption(this.mhCursor, strPassword));
            AdsException.CheckACE(ACE.AdsDecryptTable(this.mhCursor));
        }

        public void CreateIndex(string strIndexName, string strExpr)
        {
            this.CreateIndex((string)null, strIndexName, strExpr, AdsExtendedReader.IndexOptions.Default, "");
        }

        public void CreateIndex(string strIndexName, string strExpr, string strCondition)
        {
            this.CreateIndex((string)null, strIndexName, strExpr, AdsExtendedReader.IndexOptions.Default, strCondition);
        }

        public void CreateIndex(
            string strIndexName,
            string strExpr,
            AdsExtendedReader.IndexOptions eOptions)
        {
            this.CreateIndex((string)null, strIndexName, strExpr, eOptions, "");
        }

        public void CreateIndex(
            string strFileName,
            string strIndexName,
            string strExpr,
            AdsExtendedReader.IndexOptions eOptions)
        {
            this.CreateIndex(strFileName, strIndexName, strExpr, eOptions, "");
        }

        public void CreateIndex(
            string strFileName,
            string strIndexName,
            string strExpr,
            AdsExtendedReader.IndexOptions eOptions,
            string strCondition)
        {
            this.CheckOpen();
            try
            {
                this.miProgress = 0;
                this.mbAttemptCancel = false;
                this.miCurrentTag = 0;
                this.miNumTags = 1;
                if (this.mCmdCallback == null)
                    this.mCmdCallback = new ACE.CallbackFn(this.ProgressCallback);
                AdsException.CheckACE(ACE.AdsRegisterCallbackFunction(this.mCmdCallback, 1U));
                AdsException.CheckACE(ACE.AdsCreateIndex(this.mhCursor, strFileName, strIndexName, strExpr,
                    strCondition, (string)null, (uint)(ushort)eOptions, out IntPtr _));
            }
            finally
            {
                int num = (int)ACE.AdsClearCallbackFunction();
            }
        }

        private void VerifyIndexActive()
        {
            this.CheckOpen();
            if (this.mhActiveHandle == this.mhCursor)
                throw new InvalidOperationException("There is no active index.");
        }

        public void DeleteIndex()
        {
            this.VerifyIndexActive();
            AdsException.CheckACE(ACE.AdsDeleteIndex(this.mhActiveHandle));
            this.mhActiveHandle = this.mhCursor;
            this.GotoBOF();
        }

        private void OnProgressMessage(AdsInfoMessageEventArgs eventArgs)
        {
            AdsInfoMessageEventHandler progressMessage = this.ProgressMessage;
            if (progressMessage == null)
                return;
            progressMessage((object)this, eventArgs);
        }

        internal void FireProgress(int iProgress)
        {
            this.OnProgressMessage(new AdsInfoMessageEventArgs(iProgress, "Progress"));
        }

        private uint ProgressCallback(ushort usPercentDone, uint ulCallbackID)
        {
            if (usPercentDone > (ushort)100)
                usPercentDone = (ushort)100;
            this.miProgress = (int)((double)this.miCurrentTag * 100.0 / (double)this.miNumTags +
                                    (double)((int)usPercentDone / this.miNumTags));
            if (usPercentDone == (ushort)100)
                ++this.miCurrentTag;
            if (this.miCurrentTag == this.miNumTags && usPercentDone == (ushort)100)
                this.miProgress = 100;
            this.FireProgress(this.miProgress);
            return this.mbAttemptCancel ? 1U : 0U;
        }

        public void Cancel() => this.mbAttemptCancel = true;

        public int Progress => this.miProgress;

        public void OpenIndex(string strFileName)
        {
            this.CheckOpen();
            ushort pusArrayLen = 0;
            uint ulRet = ACE.AdsOpenIndex(this.mhCursor, strFileName, (IntPtr[])null, ref pusArrayLen);
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
            this.CheckOpen();
            ushort pusNum;
            AdsException.CheckACE(ACE.AdsGetNumIndexes(this.mhCursor, out pusNum));
            IntPtr[] ahIndex = new IntPtr[(int)pusNum];
            AdsException.CheckACE(ACE.AdsGetAllIndexes(this.mhCursor, ahIndex, ref pusNum));
            ArrayList arrayList = new ArrayList((int)pusNum);
            char[] pucName = new char[128];
            for (ushort index = 0; (int)index < (int)pusNum; ++index)
            {
                ushort pusLen = 128;
                AdsException.CheckACE(ACE.AdsGetIndexName(ahIndex[(int)index], pucName, ref pusLen));
                arrayList.Add((object)new string(pucName, 0, (int)pusLen));
            }

            return arrayList.ToArray();
        }

        public override bool Read() => this.Read(this.mhActiveHandle);

        public bool BOF => this.mbBOF;

        public bool EOF => this.mbEOF;

        private void CheckForBofEof()
        {
            ushort pbEof;
            AdsException.CheckACE(ACE.AdsAtEOF(this.mhCursor, out pbEof));
            ushort pbBof;
            AdsException.CheckACE(ACE.AdsAtBOF(this.mhCursor, out pbBof));
            this.mbEOF = pbEof != (ushort)0;
            this.mbBOF = pbBof != (ushort)0;
        }

        public AdsBookmark GetBookmark()
        {
            this.CheckOpen();
            return new AdsBookmark(this.mhCursor);
        }

        public void GotoBookmark(AdsBookmark bookmark)
        {
            this.CheckOpen();
            bookmark.Goto();
            this.InvalidateRecord();
            this.CheckForBofEof();
        }

        public int CompareBookmarks(AdsBookmark bmk1, AdsBookmark bmk2)
        {
            this.CheckOpen();
            int plResult = 0;
            AdsException.CheckACE(ACE.AdsCompareBookmarks(bmk1.Bookmark, bmk2.Bookmark, out plResult));
            return plResult;
        }

        public int RecordNumber
        {
            get
            {
                this.CheckOpen();
                if (this.mbBOF || this.mbEOF)
                    return 0;
                uint pulRec;
                AdsException.CheckACE(ACE.AdsGetRecordNum(this.mhCursor, (ushort)2, out pulRec));
                return (int)pulRec;
            }
            set
            {
                this.CheckOpen();
                AdsException.CheckACE(ACE.AdsGotoRecord(this.mhCursor, (uint)value));
                this.InvalidateRecord();
                this.CheckForBofEof();
            }
        }

        public int LogicalRecordNumber
        {
            get
            {
                this.CheckOpen();
                if (this.mbBOF || this.mbEOF)
                    return 0;
                uint logicalRecordNumber;
                AdsException.CheckACE(!(this.mhActiveHandle == this.mhCursor)
                    ? ACE.AdsGetKeyNum(this.mhActiveHandle, (ushort)1, out logicalRecordNumber)
                    : ACE.AdsGetRecordNum(this.mhActiveHandle, (ushort)1, out logicalRecordNumber));
                return (int)logicalRecordNumber;
            }
        }

        public int GetRecordCount(AdsExtendedReader.FilterOption eOption)
        {
            return this.GetRecordCount(eOption, false);
        }

        public int GetRecordCount(AdsExtendedReader.FilterOption eOption, bool bRefreshCount)
        {
            this.CheckOpen();
            bool flag = true;
            if (eOption == AdsExtendedReader.FilterOption.RespectFilters && this.mhActiveHandle != this.mhCursor)
            {
                uint pulFlags;
                AdsException.CheckACE(ACEUNPUB.AdsGetIndexFlags(this.mhActiveHandle, out pulFlags));
                if ((this.msTableType == (short)3 || ((int)pulFlags & 1) != 1) && this.IndexCondition.Length == 0)
                {
                    ushort pusBufLen = 4082;
                    uint scope = ACE.AdsGetScope(this.mhActiveHandle, (ushort)1, new char[(int)pusBufLen],
                        ref pusBufLen);
                    if (scope == 5038U)
                        flag = false;
                    else
                        AdsException.CheckACE(scope);
                }
            }

            ushort usFilterOption = (ushort)eOption;
            if (bRefreshCount)
                usFilterOption |= (ushort)4;
            IntPtr zero = IntPtr.Zero;
            IntPtr hTable = !flag ? this.mhCursor : this.mhActiveHandle;
            uint pulCount = 0;
            AdsException.CheckACE(ACE.AdsGetRecordCount(hTable, usFilterOption, out pulCount));
            return (int)pulCount;
        }

        public void GotoBottom()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsGotoBottom(this.mhActiveHandle));
            this.InvalidateRecord();
            this.CheckForBofEof();
        }

        public void GotoBOF()
        {
            this.CheckOpen();
            this.SetBOF();
            this.InvalidateRecord();
        }

        public void GotoTop()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsGotoTop(this.mhActiveHandle));
            this.InvalidateRecord();
            this.CheckForBofEof();
        }

        public string ActiveIndex
        {
            get
            {
                this.VerifyIndexActive();
                ushort pusLen = 128;
                char[] pucName = new char[(int)pusLen];
                AdsException.CheckACE(ACE.AdsGetIndexName(this.mhActiveHandle, pucName, ref pusLen));
                return new string(pucName, 0, (int)pusLen);
            }
            set
            {
                if (value == null || value == "")
                {
                    this.mhActiveHandle = this.mhCursor;
                }
                else
                {
                    this.CheckOpen();
                    IntPtr phIndex;
                    AdsException.CheckACE(ACE.AdsGetIndexHandle(this.mhCursor, value, out phIndex));
                    this.mhActiveHandle = phIndex;
                }

                this.GotoBOF();
            }
        }

        public double RelativeKeyPosition
        {
            get
            {
                this.VerifyIndexActive();
                double pdPos;
                AdsException.CheckACE(ACE.AdsGetRelKeyPos(this.mhActiveHandle, out pdPos));
                return pdPos;
            }
            set
            {
                this.CheckOpen();
                if (this.mhActiveHandle == this.mhCursor)
                    throw new InvalidOperationException("Invalid handle.");
                AdsException.CheckACE(ACE.AdsSetRelKeyPos(this.mhActiveHandle, value));
                this.InvalidateRecord();
                this.CheckForBofEof();
            }
        }

        private byte[] BuildRawKey(object[] keyValues, out ushort usKeyLen)
        {
            this.CheckOpen();
            ushort length;
            AdsException.CheckACE(ACEUNPUB.AdsGetNumSegments(this.mhActiveHandle, out length));
            if (keyValues.Length > (int)length)
                throw new ArgumentException("Too many objects in argument array.");
            AdsException.CheckACE(ACE.AdsInitRawKey(this.mhActiveHandle));
            ushort[] pusSegFieldNumbers = new ushort[(int)length];
            AdsException.CheckACE(
                ACEUNPUB.AdsGetSegmentFieldNumbers(this.mhActiveHandle, out length, pusSegFieldNumbers));
            try
            {
                this.mbSettingRawKey = true;
                for (ushort index = 0; (int)index < keyValues.Length; ++index)
                {
                    if (keyValues.Length > (int)index)
                    {
                        if (keyValues[(int)index] == null || keyValues[(int)index] == DBNull.Value)
                            AdsException.CheckACE(ACE.AdsSetEmpty(this.mhActiveHandle,
                                (uint)pusSegFieldNumbers[(int)index]));
                        else if (Type.GetTypeCode(keyValues[(int)index].GetType()) == TypeCode.String)
                        {
                            string pwcBuf = (string)keyValues[(int)index];
                            int num;
                            if (this.PartialMatch)
                            {
                                num = pwcBuf.Length;
                            }
                            else
                            {
                                num = this.GetACEFieldLength((int)pusSegFieldNumbers[(int)index] - 1);
                                pwcBuf = pwcBuf.PadRight(num);
                            }

                            AdsException.CheckACE(ACE.AdsSetStringW(this.mhActiveHandle,
                                (uint)pusSegFieldNumbers[(int)index], pwcBuf, (uint)num));
                        }
                        else
                            this.SetValue((int)pusSegFieldNumbers[(int)index] - 1, keyValues[(int)index]);
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
                this.mbSettingRawKey = false;
            }

            usKeyLen = (ushort)0;
            AdsException.CheckACE(ACE.AdsGetKeyLength(this.mhActiveHandle, out usKeyLen));
            byte[] pucKey = new byte[(int)usKeyLen];
            AdsException.CheckACE(ACE.AdsBuildRawKey(this.mhActiveHandle, pucKey, ref usKeyLen));
            return pucKey;
        }

        public bool PartialMatch
        {
            get => this.mbPartialMatch;
            set => this.mbPartialMatch = value;
        }

        public void SetRange(object[] topScope, object[] bottomScope)
        {
            this.VerifyIndexActive();
            if (topScope != null && topScope.Length > 0)
            {
                ushort usKeyLen;
                AdsException.CheckACE(ACE.AdsSetScope(this.mhActiveHandle, (ushort)1,
                    this.BuildRawKey(topScope, out usKeyLen), usKeyLen, (ushort)1));
            }
            else
            {
                uint ulRet = ACE.AdsClearScope(this.mhActiveHandle, (ushort)1);
                if (ulRet != 5038U)
                    AdsException.CheckACE(ulRet);
            }

            if (bottomScope != null && bottomScope.Length > 0)
            {
                ushort usKeyLen;
                AdsException.CheckACE(ACE.AdsSetScope(this.mhActiveHandle, (ushort)2,
                    this.BuildRawKey(bottomScope, out usKeyLen), usKeyLen, (ushort)1));
            }
            else
            {
                uint ulRet = ACE.AdsClearScope(this.mhActiveHandle, (ushort)2);
                if (ulRet != 5038U)
                    AdsException.CheckACE(ulRet);
            }

            this.GotoBOF();
        }

        public void ClearRange()
        {
            this.CheckOpen();
            uint ulRet1 = ACE.AdsClearScope(this.mhActiveHandle, (ushort)1);
            if (ulRet1 != 5038U)
                AdsException.CheckACE(ulRet1);
            uint ulRet2 = ACE.AdsClearScope(this.mhActiveHandle, (ushort)2);
            if (ulRet2 != 5038U)
                AdsException.CheckACE(ulRet2);
            this.GotoBOF();
        }

        public bool Seek(object[] seekValue, AdsExtendedReader.SeekType seekType)
        {
            this.VerifyIndexActive();
            if (seekValue == null || seekValue.Length == 0)
                throw new ArgumentException("Seek called with no seek value.");
            ushort pbFound = 0;
            switch (seekType)
            {
                case AdsExtendedReader.SeekType.SoftSeek:
                case AdsExtendedReader.SeekType.HardSeek:
                case AdsExtendedReader.SeekType.SeekGT:
                    ushort usKeyLen1;
                    AdsException.CheckACE(ACE.AdsSeek(this.mhActiveHandle, this.BuildRawKey(seekValue, out usKeyLen1),
                        usKeyLen1, (ushort)1, (ushort)seekType, out pbFound));
                    break;
                case AdsExtendedReader.SeekType.SeekLast:
                    ushort usKeyLen2;
                    AdsException.CheckACE(ACE.AdsSeekLast(this.mhActiveHandle,
                        this.BuildRawKey(seekValue, out usKeyLen2), usKeyLen2, (ushort)1, out pbFound));
                    break;
                default:
                    throw new NotSupportedException("Unexpected SeekType.");
            }

            this.InvalidateRecord();
            this.CheckForBofEof();
            return pbFound != (ushort)0;
        }

        public bool IsIndexUnique
        {
            get
            {
                this.VerifyIndexActive();
                ushort pbUnique;
                int num = (int)ACE.AdsIsIndexUnique(this.mhActiveHandle, out pbUnique);
                return pbUnique != (ushort)0;
            }
        }

        public bool IsIndexCandidate
        {
            get
            {
                this.VerifyIndexActive();
                ushort pbCandidate;
                int num = (int)ACE.AdsIsIndexCandidate(this.mhActiveHandle, out pbCandidate);
                return pbCandidate != (ushort)0;
            }
        }

        public bool IsIndexCompound
        {
            get
            {
                this.VerifyIndexActive();
                ushort pbCompound;
                int num = (int)ACE.AdsIsIndexCompound(this.mhActiveHandle, out pbCompound);
                return pbCompound != (ushort)0;
            }
        }

        public bool IsIndexCustom
        {
            get
            {
                this.VerifyIndexActive();
                ushort pbCustom;
                int num = (int)ACE.AdsIsIndexCustom(this.mhActiveHandle, out pbCustom);
                return pbCustom != (ushort)0;
            }
        }

        public bool IsIndexDescending
        {
            get
            {
                this.VerifyIndexActive();
                ushort pbDescending;
                int num = (int)ACE.AdsIsIndexDescending(this.mhActiveHandle, out pbDescending);
                return pbDescending != (ushort)0;
            }
        }

        public bool IsIndexPrimaryKey
        {
            get
            {
                this.VerifyIndexActive();
                ushort pbPrimaryKey;
                int num = (int)ACE.AdsIsIndexPrimaryKey(this.mhActiveHandle, out pbPrimaryKey);
                return pbPrimaryKey != (ushort)0;
            }
        }

        public string IndexExpression
        {
            get
            {
                this.VerifyIndexActive();
                ushort pusLen = 510;
                char[] pucExpr = new char[(int)pusLen];
                uint indexExpr = ACE.AdsGetIndexExpr(this.mhActiveHandle, pucExpr, ref pusLen);
                if (indexExpr == 5005U)
                {
                    pucExpr = new char[(int)pusLen];
                    indexExpr = ACE.AdsGetIndexExpr(this.mhActiveHandle, pucExpr, ref pusLen);
                }

                AdsException.CheckACE(indexExpr);
                return new string(pucExpr, 0, (int)pusLen);
            }
        }

        public string IndexCondition
        {
            get
            {
                this.VerifyIndexActive();
                ushort pusLen = 510;
                char[] pucExpr = new char[(int)pusLen];
                uint indexCondition = ACE.AdsGetIndexCondition(this.mhActiveHandle, pucExpr, ref pusLen);
                if (indexCondition == 5005U)
                {
                    pucExpr = new char[(int)pusLen];
                    indexCondition = ACE.AdsGetIndexCondition(this.mhActiveHandle, pucExpr, ref pusLen);
                }

                AdsException.CheckACE(indexCondition);
                return new string(pucExpr, 0, (int)pusLen);
            }
        }

        public string Filter
        {
            get
            {
                uint pulLen = 0;
                uint aoF100 = ACE.AdsGetAOF100(this.mhCursor, 8192U, (char[])null, ref pulLen);
                switch (aoF100)
                {
                    case 5005:
                        char[] pvFilter = new char[pulLen];
                        AdsException.CheckACE(ACE.AdsGetAOF100(this.mhCursor, 8192U, pvFilter, ref pulLen));
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
                this.CheckOpen();
                if (value == null || value.Length == 0)
                {
                    AdsException.CheckACE(ACE.AdsClearAOF(this.mhCursor));
                }
                else
                {
                    AdsException.CheckACE(ACE.AdsSetExact22(this.mhCursor,
                        this.mbPartialMatch ? (ushort)0 : (ushort)1));
                    AdsException.CheckACE(ACE.AdsSetAOF100(this.mhCursor, value, 8194U));
                }

                this.GotoBOF();
            }
        }

        public void LockRecord() => this.LockRecord(0);

        public void LockRecord(int iRecordNumber)
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsLockRecord(this.mhCursor, (uint)iRecordNumber));
        }

        public void UnlockRecord() => this.UnlockRecord(0);

        public void UnlockRecord(int iRecordNumber)
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsUnlockRecord(this.mhCursor, (uint)iRecordNumber));
        }

        public bool IsRecordLocked() => this.IsRecordLocked(0);

        public bool IsRecordLocked(int iRecordNumber)
        {
            this.CheckOpen();
            ushort pbLocked;
            AdsException.CheckACE(ACE.AdsIsRecordLocked(this.mhCursor, (uint)iRecordNumber, out pbLocked));
            return pbLocked != (ushort)0;
        }

        public void LockTable()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsLockTable(this.mhCursor));
        }

        public void UnlockTable()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsUnlockTable(this.mhCursor));
        }

        public bool IsTableLocked()
        {
            this.CheckOpen();
            ushort pbLocked;
            AdsException.CheckACE(ACE.AdsIsTableLocked(this.mhCursor, out pbLocked));
            return pbLocked != (ushort)0;
        }

        public void PackTable()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsPackTable(this.mhCursor));
            this.InvalidateRecord();
            this.CheckForBofEof();
        }

        public void ZapTable()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsZapTable(this.mhCursor));
            this.InvalidateRecord();
            this.CheckForBofEof();
        }

        public void Reindex() => this.Reindex(0);

        public void Reindex(int iPageSize)
        {
            this.CheckOpen();
            if (iPageSize < 0)
                throw new ArgumentException("Page Size cannot be negative.");
            try
            {
                this.miProgress = 0;
                this.mbAttemptCancel = false;
                this.miCurrentTag = 0;
                ushort pusNum;
                AdsException.CheckACE(ACE.AdsGetNumIndexes(this.mhCursor, out pusNum));
                this.miNumTags = (int)pusNum;
                AdsException.CheckACE(ACE.AdsGetNumFTSIndexes(this.mhCursor, out pusNum));
                this.miNumTags += (int)pusNum;
                if (this.mCmdCallback == null)
                    this.mCmdCallback = new ACE.CallbackFn(this.ProgressCallback);
                AdsException.CheckACE(ACE.AdsRegisterCallbackFunction(this.mCmdCallback, 1U));
                AdsException.CheckACE(ACE.AdsReindex61(this.mhCursor, (uint)iPageSize));
                this.InvalidateRecord();
                this.CheckForBofEof();
            }
            finally
            {
                int num = (int)ACE.AdsClearCallbackFunction();
            }
        }

        public void RecallAllRecords()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsRecallAllRecords(this.mhActiveHandle));
            this.InvalidateRecord();
            this.CheckForBofEof();
        }

        private void CheckForSet(int iCol)
        {
            this.CheckOpen();
            this.CheckPositioned();
            this.CheckColumnIndex(iCol);
        }

        public new object this[int i]
        {
            get => this.GetValue(i);
            set => this.SetValue(i, value);
        }

        public new object this[string strName]
        {
            get => this[this.GetOrdinal(strName)];
            set => this.SetValue(this.GetOrdinal(strName), value);
        }

        private void SetFieldNullOrEmpty(IntPtr hHandle, int iCol)
        {
            uint ulRet = ACE.AdsSetNull(hHandle, (uint)(iCol + 1));
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
                    this.CheckForSet(iCol);
                    this.SetFieldNullOrEmpty(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor, iCol);
                    break;
                case TypeCode.Object:
                    Type type = value.GetType();
                    if ((object)type == (object)Type.GetType("System.Byte[]"))
                    {
                        this.SetBytes(iCol, (byte[])value);
                        break;
                    }

                    if ((object)type == (object)Type.GetType("System.Char[]"))
                    {
                        this.SetChars(iCol, (char[])value);
                        break;
                    }

                    if ((object)type != (object)Type.GetType("System.TimeSpan"))
                        throw new SystemException("Invalid data type.");
                    this.SetTimeSpan(iCol, (TimeSpan)value);
                    break;
                case TypeCode.Boolean:
                    this.SetBoolean(iCol, (bool)value);
                    break;
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                    this.SetByte(iCol, (byte)value);
                    break;
                case TypeCode.Int16:
                    this.SetInt16(iCol, (short)value);
                    break;
                case TypeCode.UInt16:
                case TypeCode.Int32:
                    this.SetInt32(iCol, (int)value);
                    break;
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    this.SetInt64(iCol, (long)value);
                    break;
                case TypeCode.Single:
                    this.SetFloat(iCol, (float)value);
                    break;
                case TypeCode.Double:
                    this.SetDouble(iCol, (double)value);
                    break;
                case TypeCode.Decimal:
                    this.SetDecimal(iCol, Convert.ToDecimal(value));
                    break;
                case TypeCode.DateTime:
                    this.SetDateTime(iCol, (DateTime)value);
                    break;
                case TypeCode.String:
                    this.SetString(iCol, (string)value);
                    break;
                default:
                    throw new NotSupportedException("Unexpected object type.");
            }
        }

        public void SetBoolean(int iCol, bool value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetLogical(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), value ? (ushort)1 : (ushort)0));
            this.InvalidateRecord();
        }

        public void SetByte(int iCol, byte value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetShort(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), (short)value));
            this.InvalidateRecord();
        }

        public void SetBytes(int iCol, byte[] value)
        {
            int length = value.Length;
            this.SetBytes(iCol, 0, length, value, 0, length);
        }

        public void SetBytes(
            int iCol,
            int iFieldOffset,
            int iTotalLength,
            byte[] buffer,
            int iBufferOffset,
            int iLength)
        {
            this.CheckForSet(iCol);
            IntPtr num = this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor;
            if (iLength == 0)
            {
                this.SetFieldNullOrEmpty(num, iCol);
            }
            else
            {
                if (iBufferOffset >= buffer.Length)
                    throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                if (iLength + iBufferOffset > buffer.Length)
                    throw new IndexOutOfRangeException(
                        "The buffer offset plus the amount to write exceeds the array size.");
                ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
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

                        byte[] numArray = new byte[iLength];
                        Array.Copy((Array)buffer, iBufferOffset, (Array)numArray, 0, iLength);
                        AdsException.CheckACE(ACE.AdsSetBinary(num, (uint)(iCol + 1), (ushort)6, (uint)iTotalLength,
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

                        byte[] destinationArray = new byte[iLength];
                        Array.Copy((Array)buffer, iBufferOffset, (Array)destinationArray, 0, iLength);
                        AdsException.CheckACE(ACE.AdsSetField(num, (uint)(iCol + 1), buffer, (uint)iLength));
                        break;
                    default:
                        throw new InvalidCastException("Invalid field type.");
                }
            }

            this.InvalidateRecord();
        }

        public void SetChars(int iCol, char[] value)
        {
            this.CheckForSet(iCol);
            IntPtr hObj = this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor;
            string pwcBuf = new string(value);
            AdsException.CheckACE(ACE.AdsSetStringW(hObj, (uint)(iCol + 1), pwcBuf, (uint)pwcBuf.Length));
            this.InvalidateRecord();
        }

        public void SetInt16(int iCol, short value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetLong(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), (int)value));
            this.InvalidateRecord();
        }

        public void SetInt32(int iCol, int value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetDouble(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), (double)value));
            this.InvalidateRecord();
        }

        public void SetInt64(int iCol, long value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetLongLong(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), value));
            this.InvalidateRecord();
        }

        public void SetFloat(int iCol, float value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetDouble(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), (double)value));
            this.InvalidateRecord();
        }

        public void SetDouble(int iCol, double value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetDouble(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), value));
            this.InvalidateRecord();
        }

        public void SetString(int iCol, string value)
        {
            this.CheckForSet(iCol);
            AdsException.CheckACE(ACE.AdsSetStringW(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), value, (uint)value.Length));
            this.InvalidateRecord();
        }

        public void SetDecimal(int iCol, Decimal value)
        {
            this.CheckForSet(iCol);
            ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
            IntPtr hObj = this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor;
            if (aceFieldType == (ushort)18)
            {
                long oaCurrency = Decimal.ToOACurrency(value);
                AdsException.CheckACE(ACE.AdsSetMoney(hObj, (uint)(iCol + 1), oaCurrency));
            }
            else
            {
                string pwcBuf = value.ToString((IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
                AdsException.CheckACE(ACE.AdsSetFieldW(hObj, (uint)(iCol + 1), pwcBuf, (uint)pwcBuf.Length));
            }

            this.InvalidateRecord();
        }

        public void SetDateTime(int iCol, DateTime value)
        {
            this.CheckForSet(iCol);
            string pucJulian = value.ToString("yyyyMMdd");
            double pdJulian;
            AdsException.CheckACE(ACEUNPUB.AdsConvertStringToJulian(pucJulian, (ushort)pucJulian.Length, out pdJulian));
            IntPtr hObj = this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor;
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 3:
                    AdsException.CheckACE(ACE.AdsSetJulian(hObj, (uint)(iCol + 1), (int)pdJulian));
                    break;
                case 14:
                case 22:
                    int num1 = ((value.Hour * 60 + value.Minute) * 60 + value.Second) * 1000 + value.Millisecond;
                    ulong num2 = (ulong)pdJulian;
                    ulong num3 = (ulong)num1;
                    ulong pucBuf = !BitConverter.IsLittleEndian ? num2 << 32 | num3 : num3 << 32 | num2;
                    AdsException.CheckACE(ACEUNPUB.AdsSetTimeStampRaw(hObj, (uint)(iCol + 1), ref pucBuf, 8U));
                    break;
                default:
                    throw new NotSupportedException("Invalid field type.");
            }

            this.InvalidateRecord();
        }

        public void SetTimeSpan(int iCol, TimeSpan value)
        {
            this.CheckForSet(iCol);
            int lTime = value.Days == 0
                ? (int)value.TotalMilliseconds
                : throw new ArgumentException("TimeSpan exceeds 1 day. Cannot set time field.");
            if (lTime < 0)
                throw new ArgumentException("TimeSpan is negative. Cannot set time field.");
            AdsException.CheckACE(ACE.AdsSetMilliseconds(this.mbSettingRawKey ? this.mhActiveHandle : this.mhCursor,
                (uint)(iCol + 1), lTime));
            this.InvalidateRecord();
        }

        public void AppendRecord()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsAppendRecord(this.mhCursor));
            this.mbBOF = this.mbEOF = false;
        }

        public void DeleteRecord()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsDeleteRecord(this.mhCursor));
        }

        public bool IsRecordDeleted()
        {
            this.CheckOpen();
            ushort pbDeleted;
            AdsException.CheckACE(ACE.AdsIsRecordDeleted(this.mhCursor, out pbDeleted));
            return pbDeleted != (ushort)0;
        }

        public void RecallRecord()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsRecallRecord(this.mhCursor));
        }

        public void WriteRecord()
        {
            this.CheckOpen();
            AdsException.CheckACE(ACE.AdsWriteRecord(this.mhCursor));
            this.InvalidateRecord();
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