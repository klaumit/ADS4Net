using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Advantage.Data.Provider
{
    public class AdsParameterCollection : DbParameterCollection, IDataParameterCollection,
        IList, ICollection, IEnumerable
    {
        private ArrayList mParams;

        private ArrayList ParamList
        {
            get
            {
                if (mParams == null)
                    mParams = [];
                return mParams;
            }
        }

        public override int Count => mParams == null ? 0 : mParams.Count;

        public override bool IsFixedSize => ParamList.IsFixedSize;

        public override bool IsReadOnly => ParamList.IsReadOnly;

        public override bool IsSynchronized => ParamList.IsSynchronized;

        public override object SyncRoot => ParamList.SyncRoot;

        public override IEnumerator GetEnumerator() => ParamList.GetEnumerator();

        object IDataParameterCollection.this[string strParam]
        {
            get => this[strParam];
            set => this[strParam] = (AdsParameter)value;
        }

        public new AdsParameter this[string strParam]
        {
            get
            {
                var index = IndexOf(strParam);
                return index != -1
                    ? (AdsParameter)ParamList[index]
                    : throw new ArgumentOutOfRangeException(strParam,
                        "The parameter collection does not contain this parameter name.");
            }
            set
            {
                var index = IndexOf(strParam);
                if (index == -1)
                    throw new ArgumentOutOfRangeException(strParam,
                        "The parameter collection does not contain this parameter name.");
                ParamList[index] = value;
            }
        }

        private void SetParamNumber(ref AdsParameter param)
        {
            if (param.Index != 0 || param.ParameterName != null && !(param.ParameterName == ""))
                return;
            param.Index = ParamList.Count + 1;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= ParamList.Count)
                throw new ArgumentOutOfRangeException();
        }

        public new AdsParameter this[int index]
        {
            get
            {
                CheckIndex(index);
                return (AdsParameter)ParamList[index];
            }
            set
            {
                SetParamNumber(ref value);
                CheckIndex(index);
                ParamList[index] = value;
            }
        }

        protected override DbParameter GetParameter(int index)
        {
            CheckIndex(index);
            return (DbParameter)ParamList[index];
        }

        protected override DbParameter GetParameter(string strName)
        {
            var index = IndexOf(strName);
            CheckIndex(index);
            return (DbParameter)ParamList[index];
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            CheckIndex(index);
            ParamList[index] = (AdsParameter)value;
        }

        protected override void SetParameter(string strName, DbParameter value)
        {
            var index = IndexOf(strName);
            CheckIndex(index);
            ParamList[index] = (AdsParameter)value;
        }

        public override void Insert(int index, object value)
        {
            CheckIndex(index);
            ParamList.Insert(index, (AdsParameter)value);
        }

        public override bool Contains(string parameterName) => -1 != IndexOf(parameterName);

        public override bool Contains(object value) => -1 != IndexOf(value);

        public override int IndexOf(string parameterName)
        {
            var num = 0;
            foreach (DbParameter dbParameter in ParamList)
            {
                if (_cultureAwareCompare(dbParameter.ParameterName, parameterName) == 0)
                    return num;
                ++num;
            }

            return -1;
        }

        public override int IndexOf(object value)
        {
            var num = 0;
            if (value != null)
            {
                foreach (AdsParameter adsParameter in ParamList)
                {
                    if (value == adsParameter)
                        return num;
                    ++num;
                }
            }

            return -1;
        }

        public override void RemoveAt(string parameterName)
        {
            var index = IndexOf(parameterName);
            if (index == -1)
                throw new ArgumentOutOfRangeException(parameterName,
                    "The parameter collection does not contain this parameter name.");
            ParamList.RemoveAt(index);
        }

        public override void Remove(object value)
        {
            var index = IndexOf(value);
            if (index == -1)
                throw new ArgumentOutOfRangeException("", "Parameter value not found.");
            ParamList.RemoveAt(index);
        }

        public override void RemoveAt(int iIndex)
        {
            CheckIndex(iIndex);
            ParamList.RemoveAt(iIndex);
        }

        public override int Add(object value) => Add((AdsParameter)value);

        public int Add(AdsParameter value)
        {
            SetParamNumber(ref value);
            ParamList.Add(value);
            return Count - 1;
        }

        public AdsParameter Add(string parameterName, DbType type)
        {
            var adsParameter = new AdsParameter(parameterName, type);
            Add(adsParameter);
            return adsParameter;
        }

        public AdsParameter Add(string parameterName, object value)
        {
            var adsParameter = new AdsParameter(parameterName, value);
            Add(adsParameter);
            return adsParameter;
        }

        public AdsParameter Add(string parameterName, DbType dbType, int iSize, string sourceColumn)
        {
            var adsParameter = new AdsParameter(parameterName, dbType, iSize, sourceColumn);
            Add(adsParameter);
            return adsParameter;
        }

        public AdsParameter Add(int iIndex, object value)
        {
            var adsParameter = new AdsParameter(iIndex, value);
            Add(adsParameter);
            return adsParameter;
        }

        public override void AddRange(Array values)
        {
            if (values == null)
                throw new ArgumentNullException();
            foreach (AdsParameter adsParameter in values)
                Add(adsParameter);
        }

        public void CopyTo(AdsParameter[] array, int index) => CopyTo((Array)array, index);

        public override void CopyTo(Array array, int index) => ParamList.CopyTo(array, index);

        public override void Clear() => ParamList.Clear();

        private int _cultureAwareCompare(string strA, string strB)
        {
            return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB,
                CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
        }
    }
}