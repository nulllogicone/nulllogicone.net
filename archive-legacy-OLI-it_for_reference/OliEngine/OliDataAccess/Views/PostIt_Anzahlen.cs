// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     PostIt_Anzahlen.
    /// </summary>
    public class PostIt_Anzahlen : PostIt_AnzahlenDataSet
    {
        public PostIt_Anzahlen()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            string sql = "SELECT * FROM oli.PostIt_Anzahlen";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            ad.Fill(PostIt_Anzahlen);
        }

        public PostIt_Anzahlen(bool closed)
        {
            if (closed)
            {
                SqlConnection con = OliCommon.OLIsConnection;
                string sql = "SELECT * FROM oli.PostIt_Anzahlen WHERE closed <> 0";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);

                ad.Fill(PostIt_Anzahlen);
            }
            else
            {
                SqlConnection con = OliCommon.OLIsConnection;
                string sql = "SELECT * FROM oli.PostIt_Anzahlen WHERE closed = 0";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);

                ad.Fill(PostIt_Anzahlen);
            }
        }

        public PostIt_Anzahlen(int count)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            string sql = "SELECT TOP " + count + " * FROM oli.PostIt_Anzahlen ORDER BY Datum DESC";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            ad.Fill(PostIt_Anzahlen);
        }
    }
}
