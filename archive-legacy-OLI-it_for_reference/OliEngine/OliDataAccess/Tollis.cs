// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    public class Tollis : TollisDataSet
    {
        protected SqlDataAdapter tad;

        // Konstruktor ohne Parameter kann �berschrieben werden
        public Tollis()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Tollis WHERE 1 = 0";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(tad);

            tad.Fill(base.Tollis);
        }

        public Tollis(StammDataSet.StammRow stammRow, TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            string sql = "SELECT * FROM oli.Tollis WHERE StammGuid = '" + stammRow.StammGuid + "'";
            sql += " AND TopLabGuid = '" + topLabRow.TopLabGuid + "'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(tad);

            tad.Fill(base.Tollis);

            if (Tollis.Count == 0)
            {
                TollisRow tr = Tollis.NewTollisRow();
                tr.StammGuid = stammRow.StammGuid;
                tr.TopLabGuid = topLabRow.TopLabGuid;
                tr.Toll = 50;
                tr.TollText = "";
                tr.datum = DateTime.Now;

                Tollis.AddTollisRow(tr);
//				this.UpdateTollis();
            }
        }

        public int UpdateTollis()
        {
            return (tad.Update(base.Tollis));
        }

        // da das base-DataSet immer nur eine Reihe hat
        // wird folgende Eigenschaft neu gesetzt:
        // (von Table auf Row geschrumpft)
        public new TollisRow TollisRow
        {
            get
            {
                if (Tollis.Count > 0)
                {
                    return ((TollisRow) Tollis.Rows[0]);
                }
                else
                {
                    return (null);
                }
            }
        }
    }
}
