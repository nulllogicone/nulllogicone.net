// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     History.
    /// </summary>
    public class History : HistoryDataSet
    {
        private SqlDataAdapter had;

        public History()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand("SELECT * FROM oli.History ORDER BY Datum DESC", con);
            had = new SqlDataAdapter(cmd);

            SqlCommandBuilder cb = new SqlCommandBuilder(had);

            had.Fill(History);
        }

        public int UpdateHistory()
        {
            int r = had.Update(History);
            return (r);
        }
    }
}