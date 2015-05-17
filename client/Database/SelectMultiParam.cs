using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class SelectMultiParam<T, U> : ISelectParam
    {
        private string mColumn;
        private List<T> mValues;
        private Converter<T, U> mConverter;

        public SelectMultiParam(string Column)
        {
            mColumn = Column;
            mValues = new List<T>();
        }

        public SelectMultiParam(string Column, Converter<T, U> Converter)
        {
            mColumn = Column;
            mValues = new List<T>();
            mConverter = Converter;
        }

        public void AddValue(T Value)
        {
            mValues.Add(Value);
        }

        public bool HasValue
        {
            get { return mValues.Count > 0; }
        }

        public string WhereClause
        {
            get
            {
                if (!HasValue)
                    return null;

                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} IN (", mColumn);
                builder.AppendFormat("@{0}_value_0", mColumn);
                for (int i = 1; i < mValues.Count; ++i)
                    builder.AppendFormat(", @{0}_value_{1}", mColumn, i);
                builder.Append(")");
                return builder.ToString();
            }
        }

        public void Bind(MySqlCommand Command)
        {
            for (int i = 0; i < mValues.Count; ++i)
            {
                string param = String.Format("@{0}_value_{1}", mColumn, i);
                U converted;
                if (mConverter != null)
                    converted = mConverter(mValues[i]);
                else
                    converted = (U)Convert.ChangeType(mValues[i], typeof(U));
                Command.Parameters.AddWithValue(param, converted);
            }
        }
    }
}
