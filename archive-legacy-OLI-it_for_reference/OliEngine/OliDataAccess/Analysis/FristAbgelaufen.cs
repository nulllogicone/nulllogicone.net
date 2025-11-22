// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Analysis;

namespace OliEngine.OliDataAccess.Analysis
{
    /// <summary>
    ///     FristAbgelaufen.
    /// </summary>
    public class FristAbgelaufen : FristAbgelaufenDataSet
    {
        public FristAbgelaufen()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.FristAbgelaufen ";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(FristAbgelaufen);
        }

        public FristAbgelaufen(bool gemailt)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            string sql = "SELECT * FROM oli.FristAbgelaufen ";
            if (gemailt)
            {
                sql += " WHERE gemailt=1";
            }
            else
            {
                sql += " WHERE gemailt is null or gemailt = 0";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(FristAbgelaufen);
        }

        public FristAbgelaufen(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.FristAbgelaufen WHERE StammGuid = '" + stammRow.StammGuid + "'";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(FristAbgelaufen);
        }
    }
}
