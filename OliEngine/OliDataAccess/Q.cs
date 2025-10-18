// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     Q.
    /// </summary>
    public class Q : QDataSet
    {
        public Q(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            string sql = "SELECT * FROM oli.Q WHERE QID = " + stammRow.zuQID;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            ad.Fill(Q);
        }

        public Q()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            string sql = "SELECT * FROM oli.Q ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            ad.Fill(Q);
        }

        public new QRow QRow
        {
            get { return ((QRow) Q.Rows[0]); }
        }
    }
}