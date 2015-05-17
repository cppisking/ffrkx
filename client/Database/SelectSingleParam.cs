using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class SelectSingleParam<T> : ISelectParam
    {
        public enum ParamOperator
        {
            Equals,
            Like,
        }

        private string mColumn;
        private T mValue;
        private bool mIsSet;
        private ParamOperator mOperator;

        public SelectSingleParam(string Column, ParamOperator Operator)
        {
            mColumn = Column;
            mIsSet = false;
            mOperator = Operator;
        }

        public T Value
        {
            get { return mValue; }
            set { mValue = value; mIsSet = true; }
        }

        public string ParamName
        {
            get { return "@" + mColumn + "_value"; }
        }

        public void Bind(MySqlCommand Command)
        {
            if (mOperator == ParamOperator.Like)
                Command.Parameters.AddWithValue(ParamName, "%" + mValue + "%");
            else
                Command.Parameters.AddWithValue(ParamName, mValue);
        }

        public bool HasValue
        {
            get
            {
                if (!mIsSet)
                    return false;
                if (mValue == null)
                    return false;

                if (typeof(T) != typeof(string))
                    return true;

                string s = (string)Convert.ChangeType(mValue, typeof(string));
                return s != String.Empty;
            }
        }

        public string WhereClause
        {
            get
            {
                if (!HasValue)
                    return null;

                string op = (mOperator == ParamOperator.Equals) ? "=" : "LIKE";

                return String.Format("{0} {1} {2}", mColumn, op, ParamName);
            }
        }
    }
}
