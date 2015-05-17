using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class SelectBuilder
    {
        private string mTable;
        private List<string> mColumns;
        private List<ISelectParam> mParameters;

        public SelectBuilder()
        {
            mColumns = new List<string>();
            mParameters = new List<ISelectParam>();
        }

        public string Table 
        { 
            get { return mTable; }
            set { mTable = value; }
        }

        public IList<string> Columns
        {
            get { return mColumns; }
        }

        public IList<ISelectParam> Parameters
        {
            get { return mParameters; }
        }

        public IList<ISelectParam> UsedParameters
        {
            get
            {
                return mParameters.Where(x => x.HasValue).ToList();
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (mColumns.Count == 0)
                builder.Append("*");
            else
            {
                builder.Append(mColumns[0]);
                for (int i = 1; i < mColumns.Count; ++i)
                    builder.AppendFormat(", {0}", mColumns[i]);
            }
            builder.AppendFormat(" FROM {0}", mTable);

            IList<ISelectParam> used_params = UsedParameters;
            if (used_params.Count == 0)
                return builder.ToString();

            builder.AppendFormat(" WHERE ({0})", used_params[0].WhereClause);
            for (int i = 1; i < used_params.Count; ++i)
                builder.AppendFormat(" AND ({0})", used_params[i].WhereClause);
            return builder.ToString();
        }

        public void Bind(MySqlCommand Command)
        {
            foreach (ISelectParam param in UsedParameters)
                param.Bind(Command);
        }
    }
}
